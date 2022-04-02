namespace Gardenia.Data.Models
{
    public class ObservationImages
    {
        public int Id { get; set; }
        public string ObservationImage { get; set; }
        public UnitObservation Observation { get; set; }
        public int ObservationID { get; set; }
    }
}