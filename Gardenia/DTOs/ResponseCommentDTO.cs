using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardenia.DTOs
{
    public class ResponseCommentDTO
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string UserImage { get; set; }

        public DateTime CommentDate { get; set; }

        public string Content { get; set; }

        public int NewsID { get; set; }

        public string UserID { get; set; }
    }
}
