using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardenia.DTOs
{
    public class SmsRequest
    {
        public string ToPhoneNumber { get; set; }
        public string SenderId { get; set; }
        public string Body { get; set; }
    }
}
