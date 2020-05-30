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
    public interface ILoansRepository : IRepository<Loans>
    {
        Loans Get(int? id);

        IEnumerable<Loans> GetAll();

        void Add(Loans entity);

        void Save(Loans entity);

        void Remove(Loans entity);

        void Update(Loans entity);

        bool CheckIfPaid(long accountAccountNumber);

        Loans GetCurrentLoan(long accountNumber);

        int NumberOfLoanAccounts();
    }
}
