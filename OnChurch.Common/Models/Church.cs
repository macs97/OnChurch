using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnChurch.Common.Models
{
    public class Church
    {
        public int Id { get; set; }

        [MaxLength(80, ErrorMessage = "The field {0} must contain less than {1} characteres.")]
        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        [NotMapped]
        public int IdSection { get; set; }

        [JsonIgnore]
        public Section Section { get; set; }

        [JsonIgnore]
        public ICollection<User> Users { get; set; }

        [JsonIgnore]
        public ICollection<Meeting> Meetings { get; set; }

        [Display(Name = "# Users")]
        public int UsersNumber => Users == null ? 0 : Users.Count;

    }
}
