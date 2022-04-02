using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardenia.Data.Models
{
    public class Unit
    {
        public int Id { get; set; }
        
        public int SectionID { get; set; }

        [ForeignKey("SectionID")]
        public Section Section { get; set; }

        public int UnitTypeID { get; set; }

        public UnitType UnitType { get; set; }

        public int BuildingID { get; set; }

        [ForeignKey("BuildingID")]
        public Building Building { get; set; }

        public int FloarID { get; set; }

        [ForeignKey("FloarID")]
        public Floar Floar { get; set; }
        
        public string UnitNumber { get; set; }
        
        public int Status { get; set; }

        public string TotalArea { get; set; }

        //public ICollection<AppUser> Users { get; set; }

        //public List<UsersUnit> UsersUnit { get; set; }
        [ForeignKey("AppUser")]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
