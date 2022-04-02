using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardenia.DTOs
{
    public class VisitLogDTO
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string VisitorName { get; set; }
        public string OwnerName { get; set; }
        public DateTime Date { get; set; }
        public string SecurityName { get; set; }
    }
}
