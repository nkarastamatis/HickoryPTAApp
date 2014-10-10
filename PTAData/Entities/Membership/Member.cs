using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTAData.Entities
{
    /// <summary>
    /// Summary description for Mebmer
    /// </summary>
    /// 
    [Serializable]
    public class Member
    {
        public int MemberId { get; set; }
        public PersonName Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        [Required]
        public int MembershipId { get; set; }

        //[ForeignKey("MembershipId")]
        public virtual Membership Membership { get; set; }

        public Member()
        {
            //
            // TODO: Add constructor logic here
            //
            Initialize();
        }

        private void Initialize()
        {
        }
    }
}