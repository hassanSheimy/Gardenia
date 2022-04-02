using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardenia.Data.Models
{
    public class OrderImages
    {
        public int Id { get; set; }
        public string OrderImage { get; set; }
        public Order Order { get; set; }
        public int OrderID { get; set; }
    }
}
