using System;
using System.Collections.Generic;

namespace Gardenia.DTOs
{
    public class ObservationResponseDTO
    {
        public int Id { get; set; }

        //[Required(ErrorMessage = "you should insert a title")]
        //[MaxLength(200, ErrorMessage = "Max length is 200 characters")]
        public string Title { get; set; }

        //[Required(ErrorMessage = "you should insert a title")]
        //[MaxLength(800, ErrorMessage = "Max length is 800 characters")]
        public string Description { get; set; }

        public DateTime Date { get; set; }

        //[Required(ErrorMessage = "you should insert a Status")]
        public int Status { get; set; }

        public int ObservationID { get; set; }

        public IEnumerable<ObservationResponseImagesDTO> ObservationResponseImages { get; set; }
    }
}