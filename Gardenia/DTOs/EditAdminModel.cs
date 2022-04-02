using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gardenia.Repositories
{
    public class EditAdminModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Your full name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Your email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber, ErrorMessage = "Insert a Phone Number")]
        public string PhoneNumber { get; set; }

        public string Image { get; set; }

        public string UserName { get; set; }
    }
}
