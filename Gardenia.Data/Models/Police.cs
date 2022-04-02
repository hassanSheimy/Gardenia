using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardenia.Data.Models
{
    public class Police
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public double Rate { get; set; }
        public int RatersCount { get; set; }
        public string Late { get; set; }
        public string Long { get; set; }
        public string LeaderName { get; set; }
        public string LeaderAssistantName { get; set; }
        public string SimiLeaderAssistatnName { get; set; }
        public string LeaderPhone { get; set; }
        public string LeaderAssistantPhone { get; set; }
        public string SimiLeaderAssistatnPhone { get; set; }
    }
}
