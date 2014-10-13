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

        public Committee Find(int id)
        {
            var committee = context.Committees.Find(id);
            var post = new CommitteePost();
            post.CreatedOn = DateTime.Now;
            post.LastModified = DateTime.Now;
            committee.Posts.Add(post);
            return committee;
        }

        public Committee Find(string name)
        {
            return context.Committees.Where(c => c.CommitteeName == name).FirstOrDefault();
        }

        public void InsertOrUpdate(Committee committee)
        {
            committee.UpdateForeignKeys();
            if (committee.CommitteeId == default(int)) {
                // New entity
                committee.CreatedOn = DateTime.Now;
                committee.LastModified = committee.CreatedOn;
                context.Committees.Add(committee);
            } else {
                // Existing entity
                committee.LastModified = DateTime.Now;
                context.Entry(committee).State = System.Data.Entity.EntityState.Modified;

                if (committee.Posts != null)
                    foreach (var post in committee.Posts)
                        if (post.PostId == default(int))
                            context.Entry(post).State = System.Data.Entity.EntityState.Added;
                        else
                            context.Entry(post).State = System.Data.Entity.EntityState.Modified;

            }
        }

        public void Delete(int id)
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
        Committee Find(int id);
        Committee Find(string name);
        void InsertOrUpdate(Committee committee);
        void Delete(int id);
        void Save();
    }
}