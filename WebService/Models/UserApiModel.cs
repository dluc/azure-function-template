// Copyright (c) Microsoft. All rights reserved.

using System;
using System.Globalization;

namespace Microsoft.Azure.IoTSolutions.WebService.Models
{
    public class UserApiModel
    {
        private const string DATE_FORMAT = "yyyy-MM-dd'T'HH:mm:sszzz";

        public string Id { get; set; }
        public string DateOfBirth { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public UserApiModel(Services.Models.User value)
        {
            if (value == null) return;

            this.Id = value.Id;
            this.DateOfBirth = value.DateOfBirth.ToString(DATE_FORMAT);
            this.FirstName = value.FirstName;
            this.LastName = value.LastName;
        }

        public Services.Models.User ToServiceModel()
        {
            return new Services.Models.User
            {
                Id = this.Id,
                DateOfBirth = DateTimeOffset.ParseExact(
                    this.DateOfBirth,
                    DATE_FORMAT,
                    CultureInfo.InvariantCulture),
                FirstName = this.FirstName,
                LastName = this.LastName
            };
        }
    }
}
