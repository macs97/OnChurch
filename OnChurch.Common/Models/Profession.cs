using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnChurch.Common.Models
{
    public class Profession
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "The field {0} must contain less than {1} characteres.")]
        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<User> Users { get; set; }

        [Display(Name = "# Users")]
        public int UsersNumber => Users == null ? 0 : Users.Count;

    }
}
