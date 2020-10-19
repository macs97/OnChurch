using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnChurch.Common.Requests
{
    public class MeetingRequest
    {
        [Required]
        public DateTime Date { get; set; }
    }
}
