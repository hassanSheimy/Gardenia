using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardenia.DTOs
{
    public class GetAllOrdersForOneUser
    {
        public List<ReportDTO> Reports { get; set; }
        public List<OrderDTO> Complaints { get; set; }
        public List<OrderDTO> Suggestions { get; set; }
        public List<MaintainanceDTO> Maintainances { get; set; }
    }
}
