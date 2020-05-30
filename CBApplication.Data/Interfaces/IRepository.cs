using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Data.Entity;

using System.Threading.Tasks;

namespace CBApplication.Core.Interfaces
{
    public interface IRepository<T> where T:class
    {
        T Get(int? id);

        IEnumerable<T> GetAll();

        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        void Add(T entity);

        void Save(T entity);

        void Remove(T entity);
        void Update(T entity);

        
    }
}
