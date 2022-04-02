using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardenia.Data.Models
{
    public class Building
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Section Section { get; set; }
        public int SectionID { get; set; }
    }
}
