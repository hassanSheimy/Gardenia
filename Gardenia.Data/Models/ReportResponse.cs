using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardenia.Data.Models
{
    public class ReportResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int status { get; set; }
        public Report Report { get; set; }
        public int ReportID { get; set; }
        public List<ReportResponseImages> ReportResponseImages { get; set; }
    }
}
