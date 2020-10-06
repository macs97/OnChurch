using System.ComponentModel.DataAnnotations;

namespace OnChurch.Common.Requests
{
    public class UpdateUserRequest
    {
        [Required]
        public string Document { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public int IdProfession { get; set; }

        [Required]
        public int IdChurch { get; set; }

    }
}
