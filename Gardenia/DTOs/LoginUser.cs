using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardenia.DTOs
{
    public class LoginUser
    {
        public string UserNameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
