using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardenia.Data.Models
{
    public class WorkImageID
    {
        public string ImagePath { get; set; }

        [ForeignKey("VisitsLogs")]
        public int VisitsLogId { get; set; }
        public VisitsLogs  VisitsLogs { get; set; }
    }
}
