using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardenia.Data.Models
{
    public class Compound
    {
        public int Id { get; set; }
        public int SecurityMemberIDCounter { get; set; }
        public int AdminCounter { get; set; } = 1000;
        public int UnitsCounter { get; set; } = 1;
        public int DoneUnitsCounter { get; set; } = 1;
        public int ObservationCounter { get; set; } = 1;
        public int DoneObservationCounter { get; set; } = 1;
    }
}
