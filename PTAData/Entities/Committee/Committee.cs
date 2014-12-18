using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTAData.Entities
{

/// <summary>
/// Summary description for Committee
/// </summary>
/// 
    [Serializable]
    public class Committee : IAutoGenerateFields
    {
        public int CommitteeId { get; set; }
        public string CommitteeName { get; set; }
        
        public string Description { get; set; }

        public virtual IList<ChairPerson> ChairPersons { get; set; }
        public virtual IList<CommitteeFile> AttachedFiles { get; set; }
        public virtual IList<CommitteePost> Posts { get; set; }
        public virtual IList<CommitteeEvent> Events { get; set; }


        public Committee()
        {
            //
            // TODO: Add constructor logic here
            //
            Initialize();
        }

        public void UpdateForeignKeys()
        {
            if (ChairPersons != null)
                foreach (var chair in ChairPersons)
                    chair.CommitteeId = CommitteeId;

            if (Posts != null)
                foreach (var post in Posts)
                    post.CommitteeId = CommitteeId;

            if (Events != null)
                foreach (var e in Events)
                    e.CommitteeId = CommitteeId;

            if (AttachedFiles != null)
                foreach (var file in AttachedFiles)
                    file.CommitteeId = CommitteeId;
        }

        private void Initialize()
        {
            //ChairPersons = new List<ChairPerson>();
            //AttachedFiles = new List<CommitteeFile>();
            //Entries = new List<CommitteeEntry>();
        }

        public void SortPosts()
        {
            if (Posts != null)
            {
                Posts.OrderByDescending(p => p.CreatedOn);
            }
        }

        public void SortEvents()
        {
            if (Events != null)
            {
                Events = Events.OrderBy(e => e.EventDate).ToList();
            }
        }

        public IList<CommitteeEvent> UpcomingEvents()
        {
            return Events.Where(e => e.CompareDate() >= DateTime.Today).ToList();
        }

        public IList<CommitteeEvent> PastEvents()
        {
            return Events.Where(e => e.CompareDate() < DateTime.Today).ToList();
        }

        public DateTime LastModified { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserModified { get; set; }
    }
}