using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardenia.DTOs
{
    public class UnitDTO
    {
        public int Id { get; set; }
        public int SectionID { get; set; }
        public SectionDTO Section { get; set; }
        public int UnitTypeID { get; set; }
        public UnitTypeDTO UnitType { get; set; }
        public int BuildingID { get; set; }
        public BuildingDTO Building { get; set; }
        public int FloarID { get; set; }
        public FloarDTO Floar { get; set; }
        public string UnitNumber { get; set; }
        public string TotalArea { get; set; }
        public int Status { get; set; } = 0;
    }
}
