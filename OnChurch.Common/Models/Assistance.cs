using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnChurch.Common.Models
{
    public class Assistance
    {
        public int Id { get; set; }

        [Required]
        public User User { get; set; }

        [JsonIgnore]
        [Required]
        public Meeting Meeting { get; set; }

        [Display(Name = "Is Present")]
        public bool IsPresent { get; set; }
    }
}
