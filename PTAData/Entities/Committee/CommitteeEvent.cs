using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PTAData.Entities
{
    public class CommitteeEvent : Post
    {
        public int CommitteeId { get; set; }
        public virtual Committee Committee { get; set; }
        public DateTime EventDate { get; set; }
        public Location Location { get; set; }
    }
}
