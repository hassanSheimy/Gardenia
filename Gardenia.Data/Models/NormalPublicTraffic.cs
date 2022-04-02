using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardenia.Data.Models
{
    public class NormalPublicTraffic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public double Rate { get; set; }
        public int RatersCount { get; set; }
        public string Late { get; set; }
        public string Long { get; set; }
        public PublicTraffic PublicTraffic { get; set; }
        public int PublicTrafficID { get; set; }
    }
}
