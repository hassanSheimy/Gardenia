using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardenia.Data.Models
{
    public class GateLog
    {
        public int Id { get; set; }
        public string UserID { get; set; }
        public AppUser User { get; set; }
        public DateTime EntryDate { get; set; }
    }
}
