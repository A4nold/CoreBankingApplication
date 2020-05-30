using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBApplication.Core.Interfaces;
using CBApplication.Core.Models;

namespace CBApplication.Data.Interfaces
{
    public interface ILoanRepository : IRepository<LoanConfig>
    {
        LoanConfig Get(int? id);

        void Add(LoanConfig entity);

        void Save(LoanConfig entity);

        void Remove(LoanConfig entity);

        void Update(LoanConfig entity);

        IEnumerable<LoanConfig> GetAll();

        LoanConfig GetLoanConfig();

        LoanConfig GetLoan(int? id);

        LoanConfig GetFirst();
    }
}
