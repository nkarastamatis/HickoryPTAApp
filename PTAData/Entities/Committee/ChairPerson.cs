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
    public class ChairPerson
    {
        [Key]
        [Column(Order=1)]
        public int CommitteeId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int MemberId { get; set; }

        //[ForeignKey("MemberId")]
        public virtual Member Member { get; set; }
        //[ForeignKey("CommitteeId")]
        public virtual Committee Committee { get; set; }
    }
}
