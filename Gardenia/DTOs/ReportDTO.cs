using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gardenia.DTOs
{
    public class ReportDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "you should insert a Address")]
        [MaxLength(500, ErrorMessage = "Max length is 200 characters")]
        public string Address { get; set; }

        [Required(ErrorMessage = "you should insert a title")]
        [MaxLength(500, ErrorMessage = "Max length is 200 characters")]
        public string Topic { get; set; }

        public string Lat { get; set; }

        public string Long { get; set; }

        public int Status { get; set; }

        public DateTime Date { get; set; }

        [Required(ErrorMessage = "You should insert a report type")]
        public int ReportTypeID { get; set; }

        [Required(ErrorMessage = "You should insert a User ID")]
        public string UserID { get; set; }

        public IEnumerable<ReportImagesDTO> Images { get; set; }
        public IEnumerable<ReportResponseDTO> ReportResponses { get; set; }
    }
}
