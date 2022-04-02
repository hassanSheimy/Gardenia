using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardenia.Data.Models
{
    public class ReportResponseImages
    {
        public int Id { get; set; }
        public string RespnseImage { get; set; }
        public ReportResponse Response { get; set; }
        public int ResponseID { get; set; }
    }
}
