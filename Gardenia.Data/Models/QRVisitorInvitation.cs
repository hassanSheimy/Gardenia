using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardenia.Data.Models
{
    public class QRVisitorInvitation
    {
        public int? Id { get; set; }
        [MaxLength(50)]
        public string QRCode { get; set; }
        [Column(TypeName = "Date")]
        public DateTime Data { get; set; }
        
        [ForeignKey("Visitor")]
        public int VisitorId { get; set; }
        public Visitor Visitor { get; set; }
        
        [ForeignKey("Owner")]
        public string OwnerId { get; set; }
        public AppUser Owner { get; set; }

        public VisitsLogs VisitsLogs { get; set; }
        //public IList<VisitsLogs> VisitsLogs{ get; set; }
    }
}
