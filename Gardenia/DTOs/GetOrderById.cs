using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardenia.DTOs
{
    public class GetOrderById
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Topic { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }
        public int OrderTypeID { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
        public string UserImage { get; set; }
        public IEnumerable<OrderImagesDTO> Images { get; set; }
        public IEnumerable<OrderResponseDTO> OrderResponses { get; set; }
    }
}
