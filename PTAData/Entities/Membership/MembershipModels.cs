using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTAData.Entities
{
    /// <summary>
    /// Summary description for MembershipModels
    /// </summary>
    public class Membership
    {
        public enum MembershipType
        {
            Single,
            Family,
            Corporate
        }

        public int MembershipId { get; set; }
        public MembershipType Type { get; set; }
        public Address Address { get; set; }

        //[ForeignKey("MemberId")]
        public virtual ICollection<Member> Members { get; set; }
        
        //[ForeignKey("StudentId")]
        public virtual ICollection<Student> Students { get; set; }
        

        public Membership()
        {
            //MembershipId = Guid.NewGuid().ToString();
            //Type = MembershipType.Family;
            //Members = new List<Member>();
            //Students = new List<Student>();
            //Address = new Address();
        }
    }

    //public class MembershipContext : DbContext
    //{
    //    public MembershipContext()
    //        : base("DefaultConnection")
    //    { 
    //    }

    //    public DbSet<Membership> Memberships { get; set; }
    //    public DbSet<Member> Members { get; set; }
    //    public DbSet<Student> Students { get; set; }
    //    public DbSet<Teacher> Teachers { get; set; }
    //}
}
