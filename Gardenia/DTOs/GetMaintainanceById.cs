using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardenia.DTOs
{
    public class GetMaintainanceById
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Topic { get; set; }
        public int UnitID { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }
        public int ReportTypeID { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string UserImage { get; set; }
        public IEnumerable<MaintainanceImagesDTO> Images { get; set; }
        public IEnumerable<MaintainanceResponseDTO> ReportResponses { get; set; }
        public string UserEmail { get; internal set; }
        public string UserPhone { get; internal set; }
    }
}
