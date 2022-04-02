using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardenia.Data.Models
{
    public class UnitObservation
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Topic { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public int Status { get; set; }
        public DateTime Date { get; set; }
        public int RoomTypeID { get; set; }
        public RoomType RoomType { get; set; }
        public ObservationType ObservationType { get; set; }
        public int ObservationTypeID { get; set; }
        public Unit Unit { get; set; }
        public int UnitID { get; set; }
        public List<ObservationImages> Images { get; set; }
        public List<ObservationResponse> ObservationResponses { get; set; }
    }
}
