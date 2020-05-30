using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBApplication.Core.Models;
using CBApplication.Data.Implementations;
using CBApplication.Data.Interfaces;

namespace CBApplicaion.Data.LogicImplementation
{
    public class LoanConfigLogic
    {
        private readonly ILoanRepository _loan = new LoanRepository(new ApplicationDbContext());
        private readonly ICustomerAccountRepository _customer = new CustomerAccountRepository(new ApplicationDbContext());

        public void SaveLoanConfig(LoanConfig config)
        {
            //saving into the database
            _loan.Add(config);
            _loan.Save(config);
        }

        public void UpdateLoanConfig(LoanConfig config)
        {
            //Updating database values
            _loan.Save(config);
        }


        public LoanConfig GetLoan(int? id)
        {
            var result = _loan.GetLoan(id);
            return result;
        }

        public IEnumerable<LoanConfig> GetAll()
        {
            var result = _loan.GetAll();
            return result;
        }

        

    }
}
