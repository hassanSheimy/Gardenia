using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gardenia.DTOs
{
    public class ElectricityDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You should insert a title")]
        [MaxLength(500, ErrorMessage = "Max length is 200 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "You should insert a description")]
        [MaxLength(2000, ErrorMessage = "Max length is 800 characters")]
        public string Description { get; set; }

        public string Image { get; set; }

        [Required(ErrorMessage = "You should insert a address")]
        [MaxLength(200, ErrorMessage = "Max length is 100 characters")]
        public string Address { get; set; }

        public double Rate { get; set; }

        public int RatersCount { get; set; }

        public string Late { get; set; }

        public string Long { get; set; }

        [DataType(DataType.PhoneNumber, ErrorMessage = "you should insert a right phone number")]
        public string ComplementPhone { get; set; }

        [DataType(DataType.PhoneNumber, ErrorMessage = "you should insert a right phone number")]
        public string ReadingPhone { get; set; }

        [DataType(DataType.PhoneNumber, ErrorMessage = "you should insert a right phone number")]
        public string MalfunctionsPhone { get; set; }
    }
}
