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
    public class MemberRepository : IMemberRepository
    {
        HickoryPTAAppContext context = new HickoryPTAAppContext();

        public IQueryable<Member> All
        {
            get { return context.Members; }
        }

        public IQueryable<Member> AllIncluding(params Expression<Func<Member, object>>[] includeProperties)
        {
            IQueryable<Member> query = context.Members;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Member Find(int id)
        {
            return context.Members.Find(id);
        }

        public void InsertOrUpdate(Member member)
        {
            if (member.MemberId == default(int)) {
                // New entity
                context.Members.Add(member);
            } else {
                // Existing entity
                context.Entry(member).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var member = context.Members.Find(id);
            context.Members.Remove(member);
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

    public interface IMemberRepository : IDisposable
    {
        IQueryable<Member> All { get; }
        IQueryable<Member> AllIncluding(params Expression<Func<Member, object>>[] includeProperties);
        Member Find(int id);
        void InsertOrUpdate(Member member);
        void Delete(int id);
        void Save();
    }
}