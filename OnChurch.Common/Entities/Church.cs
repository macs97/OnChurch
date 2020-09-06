using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnChurch.Common.Entities
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
    }
}
