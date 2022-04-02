using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardenia.Data.Models
{
    public class ReportImages
    {
        public int Id { get; set; }
        public string ReportImage { get; set; }
        public Report Report { get; set; }
        public int ReportID { get; set; }
    }
}
