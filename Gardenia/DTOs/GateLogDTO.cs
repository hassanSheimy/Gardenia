using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardenia.DTOs
{
    public class GateLogDTO
    {
        public int Id { get; set; }
        public string UserID { get; set; }
        public int? Type { get; set; }
        public DateTime EntryDate { get; set; }
        public string Image { get; internal set; }
        public string Name { get; internal set; }
    }
}
