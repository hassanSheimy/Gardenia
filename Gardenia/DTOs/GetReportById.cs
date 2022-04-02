using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardenia.DTOs
{
    public class GetReportById
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Topic { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }
        public int ReportTypeID { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string UserImage { get; set; }
        public IEnumerable<ReportImagesDTO> Images { get; set; }
        public IEnumerable<ReportResponseDTO> ReportResponses { get; set; }
        public string UserEmail { get; internal set; }
        public string UserPhone { get; internal set; }
    }
}
