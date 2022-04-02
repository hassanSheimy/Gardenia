using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardenia.DTOs
{
    public class InvitationDTO
    {
        public int? Id { get; set; }
        public string Email { get; set; }
        public int VistorId { get; set; }
        public string OwnerId { get; set; }
        public string OwnerName { get; set; }
        public string NameOrDesc { get; set; }
        public string PhoneNumber { get; set; }
        public string QRCode { get; set; }
        public DateTime Date { get; set; }
        public byte Type { get; set; }
    }
}
