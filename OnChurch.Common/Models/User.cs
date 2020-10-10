using Newtonsoft.Json;
using OnChurch.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnChurch.Common.Models
{
    public class User
    {
        [MaxLength(20)]
        [Required]
        public string Document { get; set; }

        [Display(Name = "First Name")]
        [MaxLength(50)]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(50)]
        [Required]
        public string LastName { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        [Display(Name = "Photograph")]
        public Guid PhotoId { get; set; }

        [Display(Name = "Photograph")]
        public string PhotoFullPath => PhotoId == Guid.Empty
        ? $"https://onchurchweb.azurewebsites.net/images/noimage.png"
        : $"https://onchurch.blob.core.windows.net/members/{PhotoId}";

        [Display(Name = "User Type")]
        public UserType UserType { get; set; }

        public Church Church { get; set; }

        public Profession Profession { get; set; }

        [JsonIgnore]
        [NotMapped]
        public int IdProfession { get; set; }


        [Display(Name = "User")]
        public string FullName => $"{FirstName} {LastName}";

        [Display(Name = "User")]
        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";

        [JsonIgnore]
        public ICollection<Assistance> Assistances { get; set; }
    }
}
