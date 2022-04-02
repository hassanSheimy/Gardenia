using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardenia.Data.Models
{
    public class NewsImages
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public News News { get; set; }
        public int NewsID { get; set; }
    }
}
