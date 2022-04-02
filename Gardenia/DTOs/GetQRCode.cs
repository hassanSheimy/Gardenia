using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardenia.DTOs
{
    public class GetQRCode
    {
        public string QRCode { get; set; }
        public DateTime ValidateDate { get; set; }
    }
}
