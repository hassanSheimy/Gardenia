using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardenia.Data.Models
{
    public class VisitsLogs
    {
        public int Id { get; set; }
        [Column(TypeName ="DateTime")]
        public DateTime Date { get; set; }
        
        [ForeignKey("SecurityGate")]
        public string SecurityId { get; set; }
        public AppUser SecurityGate { get; set; }


        [ForeignKey("QRVisitorInvitation")]
        public int? QRVisitorInvitationId { get; set; }
        public virtual QRVisitorInvitation QRVisitorInvitation { get; set; }

        
        [ForeignKey("SMSVisitorInvitation")]
        public int? SMSVisitorInvitationId { get; set; }
        public virtual SMSVisitorInvitation SMSVisitorInvitation { get; set; }

        public IList<WorkImageID> WorkImageIDs { get; set; }

    }
}
