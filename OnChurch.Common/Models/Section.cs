using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnChurch.Common.Models
{
    public class Section
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "The field {0} must contain less than {1} characteres.")]
        [Required]
        public string Name { get; set; }

        public ICollection<Church> Churches { get; set; }

        [DisplayName("Church Number")]
        public int ChurchesNumber => Churches == null ? 0 : Churches.Count;

        [JsonIgnore]
        [NotMapped]
        public int CampusId { get; set; }

        [JsonIgnore]
        public Campus Campus { get; set; }
    }
}
