using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardenia.DTOs
{
    public class RequestCommentDTO
    {
        //[Required(ErrorMessage = "You should insert the comment body")]
        //[MaxLength(800, ErrorMessage = "Max length is 800 character")]
        public string Content { get; set; }

        //[Required(ErrorMessage = "You should insert User ID")]
        public string UserID { get; set; }
    }
}
