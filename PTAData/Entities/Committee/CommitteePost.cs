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
    public class CommitteePost : Post
    {
        public int CommitteeId { get; set; }

        public virtual Committee Committee { get; set; }
    }
}
