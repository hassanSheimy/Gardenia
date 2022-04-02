using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardenia.Data.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int Views { get; set; }
        public bool IsActive { get; set; } = true;
        public int Likes { get; set; }
        public int DisLikes { get; set; }
    }
}
