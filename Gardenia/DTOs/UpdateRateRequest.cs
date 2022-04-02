using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gardenia.DTOs
{
    public class UpdateRateRequest
    {
        [Required(ErrorMessage = "Invalide new user rate!")]
        public double NewRate { get; set; }

        [Required(ErrorMessage = "Invalide old user rate!")]
        public double OldRate { get; set; }
    }
}
