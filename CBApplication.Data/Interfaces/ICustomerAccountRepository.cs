using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CBApplication.Core.Interfaces;
using CBApplication.Core.Models;

namespace CBApplication.Data.Interfaces
{
    public interface ICustomerAccountRepository : IRepository<CustomerAccount>
    {
        CustomerAccount Get(int? id);

        IEnumerable<CustomerAccount> GetAll();

        IEnumerable<CustomerAccount> Find(Expression<Func<Customer, bool>> predicate);

        IEnumerable<CustomerAccount> GetActiveAccounts();

        IEnumerable<CustomerAccount> GetAccountType();

        void Add(CustomerAccount entity);

        void Save(CustomerAccount entity);

        void Remove(CustomerAccount entity);

        void Update(CustomerAccount entity);

        IEnumerable<CustomerAccount> GetCurrentAccount(int id);

        int NumberOfCustomerAccounts();
    }
}
