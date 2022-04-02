using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardenia.DTOs
{
    public class RateResponse
    {
        public double TotalRate { get; set; }
        public int RatersCount { get; set; } = 2;
    }
}
