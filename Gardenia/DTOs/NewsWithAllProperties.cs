using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardenia.DTOs
{
    public class NewsWithAllProperties
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int Views { get; set; }
        public bool IsActive { get; set; }
        public int Likes { get; set; }
        public int DisLikes { get; set; }
    }
}
