using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardenia.Data.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string NationalID { get; set; }
        public string Image { get; set; }
        public bool IsRegistered { get; set; }
        public bool IsActive { get; set; }
        public string VerificationCode { get; set; }
        public string QRCode { get; set; }
        public int? Type { get; set; }
        public int? SecurityMemberID { get; set; }
        public int? UserIdentityID { get; set; }
        public DateTime? QRGenerationTime { get; set; }
        public UserIdentity UserIdentity { get; set; }
        //public ICollection<Unit> Units { get; set; }
        //public List<UsersUnit> UsersUnit { get; set; }

        public string OwnerId { get; set; }
        public AppUser Owner { get; set; }
        public IList<AppUser> Followers { get; set; }
        public IList<Unit> Units { get; set; }
        public IList<QRVisitorInvitation> QRVisitorInvitations { get; set; }
        public IList<SMSVisitorInvitation> SMSVisitorInvitations{ get; set; }
    }
}
