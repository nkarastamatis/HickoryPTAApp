using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTAData.Entities
{
    /// <summary>
    /// Summary description for Student
    /// </summary>
    public class Student
    {
        public Student()
        {
            //
            // TODO: Add constructor logic here
            //

        }

        public int StudentId { get; set; }
        public PersonName Name { get; set; }

        [Required]
        public int MembershipId { get; set; }
        //[ForeignKey("MembershipId")]
        public virtual Membership Membership { get; set; }

        [Required]
        public int TeacherId { get; set; }
        //[ForeignKey("TeacherId")]
        public virtual Teacher Teacher { get; set; }
    }
}
