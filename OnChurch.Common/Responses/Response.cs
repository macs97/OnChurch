using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace OnChurch.Common.Responses
{
    public class Response
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public object Result { get; set; }
    }
}
