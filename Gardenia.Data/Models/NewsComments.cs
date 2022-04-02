using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardenia.Data.Models
{
    public class NewsComments
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CommentDate { get; set; }
        public AppUser User { get; set; }
        public string UserID { get; set; }
        public News News { get; set; }
        public int NewsID { get; set; }
    }
}
