using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardenia.Data.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Area { get; set; }
        public int UnitID { get; set; }
        public Unit Unit { get; set; }
        public int RoomTypeID { get; set; }
        public RoomType RoomType { get; set; }
    }
}
