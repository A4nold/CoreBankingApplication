using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using CBApplication.Core.Interfaces;
using CBApplication.Core.Models;

namespace CBApplication.Data.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Customer Get(int? id);

        IEnumerable<Customer> GetAll();

        IEnumerable<Customer> Find(Expression<Func<Customer, bool>> predicate);

        void Add(Customer entity);

        void Save(Customer entity);

        void Remove(Customer entity);

        void Update(Customer entity);

        int NumberOfCustomers();

        //void Entry(Customer entity);
    }
}
