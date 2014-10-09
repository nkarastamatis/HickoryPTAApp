using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using PTAData.Entities;

namespace PTAData.Repositories
{
    public class MembershipRepository : BaseRepository<MembershipContext>
    {

        public IList<Member> Members
        {
            get
            {
                return Context.Members.ToList();
            }
        }

        public int Save(Member member)
        {
            Context.Members.Add(member);
            return Context.SaveChanges();
        }
    }
}
