using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardenia.DTOs
{
    public class UnitObservationDTO
    {
        public int Id { get; set; }

        //[Required(ErrorMessage = "you should insert a Address")]
        //[MaxLength(200, ErrorMessage = "Max length is 200 characters")]
        public string Title { get; set; }

        //[Required(ErrorMessage = "you should insert a title")]
        //[MaxLength(200, ErrorMessage = "Max length is 200 characters")]
        public string Topic { get; set; }

        public string Lat { get; set; }

        public string Long { get; set; }

        public int Status { get; set; }

        public DateTime Date { get; set; }

        public int RoomTypeID { get; set; }

        //[Required(ErrorMessage = "You should insert a report type")]
        public int ObservationTypeID { get; set; }

        //[Required(ErrorMessage = "You should insert a User ID")]
        public int UnitID { get; set; }

        public IEnumerable<ObservationImagesDTO> Images { get; set; }
        public IEnumerable<ObservationResponseDTO> ObservationResponses { get; set; }
    }
}
