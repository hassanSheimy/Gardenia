using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardenia.Data.Models
{
    public class Maintainance
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Topic { get; set; }
        public int UnitID { get; set; }
        public Unit Unit { get; set; }
        public int Status { get; set; }
        public DateTime Date { get; set; }
        public MaintainanceType ReportType { get; set; }
        public int ReportTypeID { get; set; }
        public AppUser User { get; set; }
        public string UserID { get; set; }
        public List<MaintainanceImages> Images { get; set; }
        public List<MaintainanceResponse> ReportResponses { get; set; }
    }
}
