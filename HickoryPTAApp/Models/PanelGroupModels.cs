using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PTAData.Entities;

namespace HickoryPTAApp.Models
{
    public class EventPanelGroupModel
    {
        public string GroupTitle { get; set; }
        public IList<CommitteeEvent> Events { get; set; }
    }

    public class PostPanelGroupModel
    {
        public string GroupTitle { get; set; }
        public IList<CommitteePost> Posts { get; set; }
    }
}