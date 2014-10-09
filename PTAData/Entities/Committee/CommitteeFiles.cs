using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTAData.Entities
{
    public class CommitteeFile : ServerFile
    {
        [Key]
        [Column(Order = 2)]
        public string CommitteeId { get; set; }

        public CommitteeFile()
        {

        }
    }
}
