using OnChurch.Common.Enum;
using OnChurch.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnChurch.Common.Responses
{
    public class UserResponse
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Document { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public Guid PhotoId { get; set; }

        public string PhotoFullPath => PhotoId == Guid.Empty
            ? $"https://onchurchweb.azurewebsites.net/images/noimage.png"
            : $"https://onchurch.blob.core.windows.net/members/{PhotoId}";

        public UserType UserType { get; set; }

        public Church Church { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";
    }

}
