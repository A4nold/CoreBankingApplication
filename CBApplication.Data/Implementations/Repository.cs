using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CBApplication.Core.Interfaces;
using CBApplication.Core.Models;

namespace CBApplication.Data.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }


        public T Get(int? id)
        {
            return _context.Set<T>().Find(id);
        }

        public T GetFirst()
        {
            return _context.Set<T>().ToList().FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void Save(T entity)
        {
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _context.SaveChanges();
        }

    }
}
