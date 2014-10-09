using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace PTAData.Repositories
{
    public static class Extentions
    {
        public static IQueryable IncludeAll<TEntity>(this DbSet<TEntity> dbSet) where TEntity : class
        {
            var foreignKeys = new List<string>();

            var foreignKeyProperties = typeof(TEntity).GetProperties()
                    .Where(p => p.IsDefined(typeof(ForeignKeyAttribute), true))
                    .ToList();

            foreignKeys.AddRange(
                foreignKeyProperties.Select(p => p.Name));

            foreignKeyProperties.ForEach(delegate(PropertyInfo info)
            {
                List<PropertyInfo> more = new List<PropertyInfo>();
                more.AddRange( 
                info.PropertyType.GenericTypeArguments[0].GetProperties()
                    .Where(p => p.IsDefined(typeof(ForeignKeyAttribute), true))
                    .ToList());

                foreignKeys.AddRange(
                    more.Select(p => info.Name + "." + p.Name));
            });

            return foreignKeys
                .Aggregate(
                dbSet.AsQueryable(),
                (current, key) => current.Include(key));
        }

        
    }
}
