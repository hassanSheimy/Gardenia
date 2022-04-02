using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardenia.Data.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Topic { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public int Status { get; set; }
        public DateTime Date { get; set; }
        public int OrderTypeID { get; set; }
        public AppUser User { get; set; }
        public string UserID { get; set; }
        public List<OrderImages> Images { get; set; }
        public List<OrderResponse> OrderResponses { get; set; }
    }
}
