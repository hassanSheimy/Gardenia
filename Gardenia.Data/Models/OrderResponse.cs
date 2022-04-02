using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardenia.Data.Models
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }
        public Order Order { get; set; }
        public int OrderID { get; set; }
        public List<OrderResponseImages> OrderResponseImages { get; set; }
    }
}
