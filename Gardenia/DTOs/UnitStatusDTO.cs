using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardenia.DTOs
{
    public class UnitStatusDTO
    {
        public int Id { get; set; }
        
        public string Reason { get; set; }
        
        public DateTime? AcceptanceDate { get; set; }
        
        public int Type { get; set; }
        
        public int UnitID { get; set; }
        
        public string UserID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Image { get; set; }

        public string NationalID { get; set; }
    }
}
