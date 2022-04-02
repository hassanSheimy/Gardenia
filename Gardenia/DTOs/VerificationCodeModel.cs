using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardenia.DTOs
{
    public class VerificationCodeModel
    {
        public string PhoneNumber { get; set; }
        public string VerificationCode { get; set; }
    }
}
