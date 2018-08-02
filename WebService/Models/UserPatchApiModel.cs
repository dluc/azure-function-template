// Copyright (c) Microsoft. All rights reserved.

namespace Microsoft.Azure.IoTSolutions.WebService.Models
{
    public class UserPatchApiModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public UserPatchApiModel(Services.Models.UserPatch value)
        {
            if (value == null) return;

            this.Id = value.Id;
            this.FirstName = value.FirstName;
            this.LastName = value.LastName;
        }

        public Services.Models.UserPatch ToServiceModel()
        {
            return new Services.Models.UserPatch
            {
                Id = this.Id,
                FirstName = this.FirstName,
                LastName = this.LastName
            };
        }
    }
}
