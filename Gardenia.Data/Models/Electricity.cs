using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardenia.Data.Models
{
    public class Electricity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public double Rate { get; set; }
        public int RatersCount { get; set; }
        public string Late { get; set; }
        public string Long { get; set; }
        public string ComplementPhone { get; set; }
        public string ReadingPhone { get; set; }
        public string MalfunctionsPhone { get; set; }
    }
}
