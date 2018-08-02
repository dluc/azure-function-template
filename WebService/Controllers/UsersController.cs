// Copyright (c) Microsoft. All rights reserved.

using System.Threading.Tasks;
using Microsoft.Azure.IoTSolutions.Services;
using Microsoft.Azure.IoTSolutions.Services.Diagnostics;
using Microsoft.Azure.IoTSolutions.WebService.Models;

namespace Microsoft.Azure.IoTSolutions.WebService.Controllers
{
    public class UsersController
    {
        private readonly IUsers users;
        private readonly ILogger log;

        public UsersController(IUsers users, ILogger logger)
        {
            this.users = users;
            this.log = logger;
        }

        public async Task<UserApiModel> GetAsync(string id)
        {
            return new UserApiModel(await this.users.GetAsync(id));
        }

        public async Task<UserApiModel> PostAsync(UserApiModel user)
        {
            return new UserApiModel(await this.users.CreateAsync(user.ToServiceModel()));
        }

        public async Task<UserApiModel> PutAsync(string id, UserApiModel user)
        {
            return new UserApiModel(await this.users.UpdateAsync(id, user.ToServiceModel()));
        }

        public async Task<UserApiModel> PatchAsync(string id, UserPatchApiModel patch)
        {
            return new UserApiModel(await this.users.PatchAsync(id, patch.ToServiceModel()));
        }

        public async Task DeleteAsync(string id)
        {
            await Task.CompletedTask;
        }
    }
}
