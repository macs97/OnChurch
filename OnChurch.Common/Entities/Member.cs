using Newtonsoft.Json;
using System;
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

        [Display(Name = "Photograph")]
        public Guid PhotoId { get; set; }

        [Display(Name = "Photograph")]
        public string PhotoFullPath => PhotoId == Guid.Empty
        ? $"https://localhost:44390/images/noimage.png"
        : $"https://onchurch.blob.core.windows.net/members/{PhotoId}";


        public Profession Profession { get; set; }

        [JsonIgnore]
        [NotMapped]
        public int IdProfession { get; set; }

        [JsonIgnore]
        [NotMapped]
        public int IdChurch { get; set; }
    }
}
