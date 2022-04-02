using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gardenia.DTOs
{
    public class AddRateRequest
    {
        [Required(ErrorMessage = "Invalid user rate!")]
        public double UserRate { get; set; }
    }
}
