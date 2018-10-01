using MuzON.DAL.EF;
using MuzON.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzON.DAL.Repositories
{
    class BaseRepository<T> : IRepository<T> where T : class
    {
        private MuzONContext Context;

        private DbSet<T> DbSet;

        public BaseRepository(MuzONContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        public void Create(T item)
        {
            DbSet.Add(item);
        }

        public void Delete(Guid id)
        {
            T entity = DbSet.Find(id);
            if (entity != null)
                DbSet.Remove(entity);
        }

        public T Get(Guid? id)
        {
            return DbSet.Find(id);
        }

        public virtual IQueryable<T> GetAll()
        {
            return DbSet;
        }

        public IEnumerable<T> SearchFor(Func<T, bool> predicate)
        {
            return GetAll().Where(predicate);
        }

        public void Update(T item)
        {
            var entityEntry = Context.Entry(item);
            if (entityEntry.State == EntityState.Detached)
            {
                DbSet.Attach(item);
            }
            entityEntry.State = EntityState.Modified;
        }
    }
}
