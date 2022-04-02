using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gardenia.DTOs
{
    public class AddUserModel
    {
        public string Id { get; set; }

        //[Required(ErrorMessage = "Your full name is required")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "Your email is required")]
        //[EmailAddress]
        public string Email { get; set; }

        //[Required(ErrorMessage = "You need to enter a password")]
        //[DataType(DataType.Password)]
        public string Password { get; set; } = "1548Aa@";

        //[Required(ErrorMessage = "Confirm your password")]
        //[DataType(DataType.PhoneNumber, ErrorMessage = "Insert a Phone Number")]
        public string PhoneNumber { get; set; }

        public string Image { get; set; }

        public string NationalID { get; set; }

        public string UserName { get; set; }

        public bool IsRegistered { get; set; }

        public int? Type { get; set; }

        public int? SecurityMemberID { get; set; }

        public int? UserIdentityID { get; set; }
        // owner Id
        public string OwnerId { get; set; }
    }
}
