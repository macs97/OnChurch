using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnChurch.Common.Entities
{
    public class Member
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "The field {0} must contain less than {1} characteres.")]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(50, ErrorMessage = "The field {0} must contain less than {1} characteres.")]
        [Required]
        public string LastName { get; set; }

        [MaxLength(20, ErrorMessage = "The field {0} must contain less than {1} characteres.")]
        [Required]
        public string Document { get; set; }

        [MaxLength(50, ErrorMessage = "The field {0} must contain less than {1} characteres.")]
        [Required]
        public string Address { get; set; }

        [MaxLength(20, ErrorMessage = "The field {0} must contain less than {1} characteres.")]
        [Required]
        public string Phone { get; set; }

        [MaxLength(50, ErrorMessage = "The field {0} must contain less than {1} characteres.")]
        [Required]
        public string Email { get; set; }

        public Profession Profession { get; set; }

        [JsonIgnore]
        [NotMapped]
        public int IdProfession { get; set; }

        [JsonIgnore]
        [NotMapped]
        public int IdChurch { get; set; }
    }
}
