namespace Gardenia.Data.Models
{
    public class ObservationResponseImages
    {
        public int Id { get; set; }
        public string RespnseImage { get; set; }
        public ObservationResponse Response { get; set; }
        public int ResponseID { get; set; }
    }
}