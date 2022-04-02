using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gardenia.DTOs
{
    public class LoginAdminModel
    {
        public class Request
        {
            [Required(ErrorMessage = "You should inser a user name or email")]
            public string UserNameOrEmail { get; set; }

            [Required(ErrorMessage = "Insert your password")]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }
        public class Response
        {
            public string Id { get; set; }

            public string Name { get; set; }

            public string Email { get; set; }

            public string PhoneNumber { get; set; }

            public string Image { get; set; }

            public string NationalID { get; set; }

            public int? Type { get; set; }

            public string Token { get; set; }
        }
    }
}
