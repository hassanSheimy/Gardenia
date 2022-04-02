using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gardenia.DTOs
{
    public class NormalPublicTrafficDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You should insert a title")]
        [MaxLength(500, ErrorMessage = "Max length is 200 characters")]
        public string Title { get; set; }

        public string Image { get; set; }

        [Required(ErrorMessage = "You should insert an address")]
        [MaxLength(200, ErrorMessage = "Max length is 100 characters")]
        public string Address { get; set; }

        [DataType(DataType.PhoneNumber, ErrorMessage = "you should insert a right phone number")]
        public string Phone1 { get; set; }

        [DataType(DataType.PhoneNumber, ErrorMessage = "you should insert a right phone number")]
        public string Phone2 { get; set; }

        public double Rate { get; set; }

        public int RatersCount { get; set; }

        public string Late { get; set; }

        public string Long { get; set; }

        [Required(ErrorMessage = "You should insert an Category ID")]
        public int PublicTrafficID { get; set; }
    }
}
