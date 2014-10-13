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

        //[ForeignKey("CommitteeName")]
        public virtual ICollection<ChairPerson> ChairPersons { get; set; }
        //[ForeignKey("CommitteeName")]
        public virtual ICollection<CommitteeFile> AttachedFiles { get; set; }
        //[ForeignKey("CommitteeName")]
        public virtual ICollection<CommitteePost> Posts { get; set; }


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
        }

        private void Initialize()
        {
            //ChairPersons = new List<ChairPerson>();
            //AttachedFiles = new List<CommitteeFile>();
            //Entries = new List<CommitteeEntry>();
        }

        public DateTime LastModified { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserModified { get; set; }
    }
}