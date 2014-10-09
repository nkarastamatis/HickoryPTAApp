using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTAData.Entities
{
    [Serializable]
    public class CommitteePost : SitePost
    {
        [Key]
        [Column(Order = 2)]
        public string CommitteeId { get; set; }
    }
}
