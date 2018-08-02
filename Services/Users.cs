// Copyright (c) Microsoft. All rights reserved.

using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.Azure.IoTSolutions.Services.Models;

namespace Microsoft.Azure.IoTSolutions.Services
{
    public interface IUsers
    {
        Task<User> GetAsync(string id);
        Task<User> CreateAsync(User user);
        Task<User> UpdateAsync(string id, User user);
        Task<User> PatchAsync(string id, UserPatch user);
        Task DeleteAsync(string id);
    }

    public class Users : IUsers
    {
        public async Task<User> GetAsync(string id)
        {
            var date = DateTimeOffset.ParseExact(
                "Sun 15 Jun 2000 8:30 AM -06:00",
                "ddd dd MMM yyyy h:mm tt zzz",
                CultureInfo.InvariantCulture);

            var user = new User
            {
                Id = id,
                DateOfBirth = date,
                FirstName = "Bob",
                LastName = "Foobar"
            };

            return await Task.FromResult(user);
        }

        public async Task<User> CreateAsync(User user)
        {
            return await Task.FromResult(user);
        }

        public async Task<User> UpdateAsync(string id, User user)
        {
            return await Task.FromResult(user);
        }

        public Task<User> PatchAsync(string id, UserPatch user)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(string id)
        {
            await Task.CompletedTask;
        }
    }
}
