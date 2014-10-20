﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using PTAData.Entities;

namespace HickoryPTAApp.Models
{
    public class PostRepository : IPostRepository
    {
        HickoryPTAAppContext context = new HickoryPTAAppContext();

        public IQueryable<Post> All
        {
            get { return context.Posts; }
        }

        public IQueryable<Post> AllIncluding(params Expression<Func<Post, object>>[] includeProperties)
        {
            IQueryable<Post> query = context.Posts;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Post Find(int id)
        {
            var post = context.Posts.Find(id);
            return post;
        }

        public void InsertOrUpdate(Post post, string currentUser)
        {
            if (post.PostId == default(int))
            {
                // New entity
                UpdateAutoGeneratedFields(post, currentUser, true);
                context.Posts.Add(post);
            }
            else
            {
                // Existing entity
                post.LastModified = DateTime.Now;

                UpdateAutoGeneratedFields(post, currentUser, false);
                context.Entry(post).State = System.Data.Entity.EntityState.Modified;
            }
        }

        private void UpdateAutoGeneratedFields(object obj, string user, bool newItem)
        {
            var item = obj as IAutoGenerateFields;

            if (item == null)
                return;

            if (newItem)
                item.CreatedOn = DateTime.Now;

            item.UserModified = user;
            item.LastModified = DateTime.Now;
        }

        public void Delete(int id)
        {
            var post = context.Posts.Find(id);
            context.Posts.Remove(post);
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

    public interface IPostRepository : IDisposable
    {
        IQueryable<Post> All { get; }
        IQueryable<Post> AllIncluding(params Expression<Func<Post, object>>[] includeProperties);
        Post Find(int id);
        void InsertOrUpdate(Post post, string currentUser);
        void Delete(int id);
        void Save();
    }
}