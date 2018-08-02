// Copyright (c) Microsoft. All rights reserved.

using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Azure.IoTSolutions.Services.Exceptions;
using Microsoft.Azure.IoTSolutions.WebService.Controllers;
using Microsoft.Azure.IoTSolutions.WebService.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace AzureFunction
{
    // Minimal boilerplate, delegating work to MVC type controllers.
    // Known exception types are handled here and mapped to relevant HTTP status codes.
    public static class AzureFunction
    {
        // Making container static, to hold singletons and data for the lifetime
        // of the function (between restarts)
        public static readonly IContainer container = DependencyResolution.GetCointainer();

        [FunctionName("users")]
        public static async Task<HttpResponseMessage> GetAsync(
            [HttpTrigger(
                AuthorizationLevel.Anonymous,
                "get", "post", "delete", "patch", "put",
                Route = "v1/users/{id?}")] HttpRequestMessage req,
            string id,
            TraceWriter log)
        {
            container.SetLogger(log);

            // Default response, changed later
            var result = new HttpResponseMessage { StatusCode = HttpStatusCode.MethodNotAllowed };

            // This should never fail, i.e. constructors should not have external dependencies
            var controller = container.Resolve<UsersController>();

            try
            {
                // Comment out the method which are not supported
                switch (req.Method.ToString().ToUpperInvariant())
                {
                    case "GET":
                        result = GetSuccessfulResponse(await controller.GetAsync(id));
                        break;

                    case "POST":
                        var postInput = await req.Content.ReadAsAsync<UserApiModel>();
                        result = GetSuccessfulResponse(await controller.PostAsync(postInput));
                        break;

                    case "PUT":
                        var putInput = await req.Content.ReadAsAsync<UserApiModel>();
                        result = GetSuccessfulResponse(await controller.PutAsync(id, putInput));
                        break;

                    case "PATCH":
                        var patchInput = await req.Content.ReadAsAsync<UserPatchApiModel>();
                        result = GetSuccessfulResponse(await controller.PatchAsync(id, patchInput));
                        break;

                    case "DELETE":
                        await controller.DeleteAsync(id);
                        break;
                }
            }
            catch (Exception e)
            {
                result = GetErrorResponse(e);
            }

            return result;
        }

        private static HttpResponseMessage GetSuccessfulResponse<T>(T content)
        {
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new ObjectContent(typeof(T), content, new JsonMediaTypeFormatter(), "application/json")
            };
        }

        private static HttpResponseMessage GetErrorResponse(Exception e)
        {
            // Default error
            var result = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError
            };

            // Optional error content
            object content = null;

            // Some examples...
            switch (e)
            {
                case ResourceNotFoundException _:
                    result.StatusCode = HttpStatusCode.NotFound;
                    break;

                case ConflictingResourceException _:
                    result.StatusCode = HttpStatusCode.Conflict;
                    break;

                case ExternalDependencyException _:
                    result.StatusCode = HttpStatusCode.ServiceUnavailable;
                    content = new
                    {
                        ErrorMessage = "Something went wrong with an external dependency. Please retry later."
                    };
                    break;
            }

            // JSON serialization
            result.Content = new ObjectContent(content?.GetType(), content, new JsonMediaTypeFormatter(), "application/json");

            return result;
        }
    }
}
