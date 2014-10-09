using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using PTAData.Entities;

namespace HickoryPTAApp.Models
{ 
    public class CommitteeRepository : ICommitteeRepository
    {
        HickoryPTAAppContext context = new HickoryPTAAppContext();

        public IQueryable<Committee> All
        {
            get { return context.Committees; }
        }

        public IQueryable<Committee> AllIncluding(params Expression<Func<Committee, object>>[] includeProperties)
        {
            IQueryable<Committee> query = context.Committees;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Committee Find(string id)
        {
            return context.Committees.Find(id);
        }

        public void InsertOrUpdate(Committee committee)
        {
            if (committee.CommitteeId == default(string)) {
                // New entity
                context.Committees.Add(committee);
            } else {
                // Existing entity
                context.Entry(committee).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(string id)
        {
            var committee = context.Committees.Find(id);
            context.Committees.Remove(committee);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose() 
        {
            context.Dispose();
        }
    }

    public interface ICommitteeRepository : IDisposable
    {
        IQueryable<Committee> All { get; }
        IQueryable<Committee> AllIncluding(params Expression<Func<Committee, object>>[] includeProperties);
        Committee Find(string id);
        void InsertOrUpdate(Committee committee);
        void Delete(string id);
        void Save();
    }
}