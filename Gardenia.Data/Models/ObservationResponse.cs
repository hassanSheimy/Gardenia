using System;
using System.Collections.Generic;

namespace Gardenia.Data.Models
{
    public class ObservationResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int status { get; set; }
        public UnitObservation Observation { get; set; }
        public int ObservationID { get; set; }
        public List<ObservationResponseImages> ObservationResponseImages { get; set; }
    }
}