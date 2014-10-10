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
    public class MembershipRepository : IMembershipRepository
    {
        HickoryPTAAppContext context = new HickoryPTAAppContext();

        public IQueryable<Membership> All
        {
            get { return context.Memberships; }
        }

        public IQueryable<Membership> AllIncluding(params Expression<Func<Membership, object>>[] includeProperties)
        {
            IQueryable<Membership> query = context.Memberships;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Membership Find(int id)
        {
            return context.Memberships.Find(id);
        }

        public void InsertOrUpdate(Membership membership)
        {
            if (membership.MembershipId == default(int)) {
                // New entity
                context.Memberships.Add(membership);
            } else {
                // Existing entity
                context.Entry(membership).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var membership = context.Memberships.Find(id);
            context.Memberships.Remove(membership);
        }

        public void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                int i = 0;
            }
        }

        public void Dispose() 
        {
            context.Dispose();
        }
    }

    public interface IMembershipRepository : IDisposable
    {
        IQueryable<Membership> All { get; }
        IQueryable<Membership> AllIncluding(params Expression<Func<Membership, object>>[] includeProperties);
        Membership Find(int id);
        void InsertOrUpdate(Membership membership);
        void Delete(int id);
        void Save();
    }
}