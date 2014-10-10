using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PTAData.Entities
{
    public class PTADataContext : DbContext
    {
        public PTADataContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Committee> Committees { get; set; }
        public DbSet<CommitteeFile> CommitteeFiles { get; set; }
        public DbSet<CommitteePost> CommitteeEntries { get; set; }

        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
    }
}
