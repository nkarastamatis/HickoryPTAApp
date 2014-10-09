using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PTAData.Entities;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTAData.Repositories
{
    public class CommitteeRepository : BaseRepository<CommitteeContext>
    {       
        public List<Committee> Committees
        {
            get
            {
                return Context.Committees.ToList();
            }
        }

        public int Save(Committee committee)
        {
            Context.Committees.Add(committee);
            return Context.SaveChanges();
        }
    }
}
