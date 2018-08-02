// Copyright (c) Microsoft. All rights reserved.

using System.Reflection;
using Autofac;
using Microsoft.Azure.IoTSolutions.Services;
using Microsoft.Azure.IoTSolutions.Services.Diagnostics;
using Microsoft.Azure.IoTSolutions.WebService.Controllers;
using Microsoft.Azure.WebJobs.Host;

namespace AzureFunction
{
    public static class DependencyResolution
    {
        public static IContainer GetCointainer()
        {
            var builder = new ContainerBuilder();
            AutowireAssemblies(builder);

            // Wire controllers without an interface
            builder.RegisterType<UsersController>().As<UsersController>().SingleInstance();

            // Some singletons...
            //builder.RegisterType<Counter>().As<ICounter>().SingleInstance();

            return builder.Build();
        }

        public static void SetLogger(this IContainer container, TraceWriter log)
        {
            container.Resolve<ILogger>().SetWriter(log);
        }

        private static void AutowireAssemblies(ContainerBuilder builder)
        {
            // Auto-wire Services.DLL
            var assembly = typeof(Users).GetTypeInfo().Assembly;
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces();

            // Auto-wire WebService.DLL
            assembly = typeof(UsersController).GetTypeInfo().Assembly;
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces();
        }
    }
}
