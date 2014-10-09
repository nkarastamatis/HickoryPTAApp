using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PTAData.Entities
{
    public class CommitteeContext : DbContext
    {
        public CommitteeContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Committee> Committees { get; set; }
        public DbSet<CommitteeFile> CommitteeFiles { get; set; }
        public DbSet<CommitteePost> CommitteeEntries { get; set; }
    }
}
