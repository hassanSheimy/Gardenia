using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardenia.DTOs
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public int RoomTypeID { get; set; }
        public RoomTypeDTO RoomType { get; set; }
        public string Area { get; set; }
        public int UnitID { get; set; }
    }
}
