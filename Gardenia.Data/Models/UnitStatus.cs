using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardenia.Data.Models
{
    public class UnitStatus
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public DateTime? AcceptanceDate { get; set; }
        public int Type { get; set; }
        public int UnitID { get; set; }
        public Unit Unit { get; set; }
        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public AppUser User { get; set; }
    }
}
