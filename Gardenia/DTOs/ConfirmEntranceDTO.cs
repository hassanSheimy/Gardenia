using Gardenia.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardenia.DTOs
{
    public class ConfirmEntranceDTO
    {
        public int InvitationId { get; set; }
        public byte Type { get; set; }
        public string SecurityId { get; set; }
        public DateTime EnteranceDate { get; set; }
        public List<string> Images { get; set; }
    }
}
