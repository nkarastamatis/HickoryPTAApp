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

            StudentId = Guid.NewGuid().ToString();
        }

        public string StudentId { get; set; }
        public PersonName Name { get; set; }

        [Required]
        public string MembershipId { get; set; }
        [ForeignKey("MembershipId")]
        public Membership Membership { get; set; }

        [Required]
        public string TeacherId { get; set; }
        [ForeignKey("TeacherId")]
        public Teacher Teacher { get; set; }
    }
}
