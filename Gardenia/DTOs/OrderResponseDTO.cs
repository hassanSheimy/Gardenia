using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gardenia.DTOs
{
    public class OrderResponseDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "you should insert a title")]
        [MaxLength(500, ErrorMessage = "Max length is 200 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "you should insert a description")]
        [MaxLength(2000, ErrorMessage = "Max length is 800 characters")]
        public string Description { get; set; }

        public DateTime Date { get; set; }

        [Required(ErrorMessage = "you should insert a Status")]
        public int Status { get; set; }

        public int OrderID { get; set; }

        public IEnumerable<OrderResponseImagesDTO> OrderResponseImages { get; set; }
    }
}
