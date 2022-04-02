using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardenia.DTOs
{
    public class GetObservationById
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Topic { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }
        public int ObservationTypeID { get; set; }
        public int RoomTypeID { get; set; }
        public int UnitID { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string UserImage { get; set; }
        public string UserEmail { get; internal set; }
        public string UserPhone { get; internal set; }
        public IEnumerable<ObservationImagesDTO> Images { get; set; }
        public IEnumerable<ObservationResponseDTO> Responses { get; set; }
    }
}
