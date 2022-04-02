using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardenia.Data.Models
{
    public class Floar
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Building Building { get; set; }
        public int BuildingID { get; set; }
    }
}
