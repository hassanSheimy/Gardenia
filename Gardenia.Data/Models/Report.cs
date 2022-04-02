using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardenia.Data.Models
{
    public class Report
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Topic { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public int Status { get; set; }
        public DateTime Date { get; set; }
        public ReportType ReportType { get; set; }
        public int ReportTypeID { get; set; }
        public AppUser User { get; set; }
        public string UserID { get; set; }
        public List<ReportImages> Images { get; set; }
        public List<ReportResponse> ReportResponses { get; set; }
    }
}
