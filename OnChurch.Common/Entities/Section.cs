using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnChurch.Common.Entities
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
    }
}
