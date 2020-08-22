using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnChurch.Common.Entities
{
    public class Church
    {
        public int Id { get; set; }

        [MaxLength(80, ErrorMessage = "The field {0} must contain less than {1} characteres.")]
        [Required]
        public string Name { get; set; }

        public ICollection<Member> Members { get; set; }

        public Profession Profession { get; set; }
    }
}
