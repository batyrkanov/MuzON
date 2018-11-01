using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzON.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T Get(Guid? id);
        IEnumerable<T> SearchFor(Func<T, Boolean> predicate);
        void Create(T item);
        void Update(T item);
        void Delete(Guid id);
    }
}
