using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardenia.Data.Models
{
    public class SMSVisitorInvitation
    {
        public int? Id { get; set; }
        public bool IsSMSVisitor { get; set; } = true;
        [StringLength(150,ErrorMessage ="Description Must Be Less Than 150 Characters")]
        public string NameOrDesc { get; set; }
        [Column(TypeName = "Date")]
        public DateTime Data { get; set; }


        [ForeignKey("Owner")]
        public string OwnerId { get; set; }
        public AppUser Owner { get; set; }

        public  VisitsLogs VisitsLogs { get; set; }

        // public IList<VisitsLogs> VisitsLogs { get; set; }

    }
}
