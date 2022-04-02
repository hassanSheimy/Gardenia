using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardenia.Data.Models
{
    public class Visitor
    {
        public int Id { get; set; }
        [Email]
        [Required]
        [MaxLength(20)]
        [Column(TypeName = "varchar(50)")]
        public string Email { get; set; }
        [MaxLength(20)]
        [Required]
        [Column(TypeName ="varchar(20)")]    
        public string UserName { get; set; }

        public IList<QRVisitorInvitation> QRVisitorInvitations { get; set; }
    }
}
