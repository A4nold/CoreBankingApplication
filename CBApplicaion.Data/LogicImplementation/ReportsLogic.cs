using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBApplication.Core.Models;
using CBApplication.Data.Implementations;

namespace CBApplicaion.Data.LogicImplementation
{
    public class ReportsLogic
    {
        private readonly GlAccountRepository _glAccount = new GlAccountRepository(new ApplicationDbContext());
        private readonly CustomerAccountRepository _customerAccount = new CustomerAccountRepository(new ApplicationDbContext());
        private readonly LoansRepository _loans = new LoansRepository(new ApplicationDbContext());

        public List<GlAccountManagement> GetAllAssetAccounts()
        {
            var assetAccounts = _glAccount.GetMainAccountCat("Asset").ToList();
            var loanAssetAcct = new GlAccountManagement
            {
                Name = "Loan Accounts"
            };
            var loanAccounts = _customerAccount.GetLoanAccounts();
            foreach (var account in loanAccounts)
            {
                loanAssetAcct.AccountBalance += account.Balance;
            }

            assetAccounts.Add(loanAssetAcct);
            return assetAccounts;
        }

        public List<GlAccountManagement> GetAllCapitalAccounts()
        {
            var capitalAccounts = _glAccount.GetMainAccountCat("Capital").ToList();
            var totalIncomeGl = _glAccount.GetMainAccountCat("Income").Sum(a => a.AccountBalance);
            var toatlExpenseGl = _glAccount.GetMainAccountCat("Expenses").Sum(a => a.AccountBalance);

            var CapitalAcctSum = new GlAccountManagement
            {
                Name = "Reserve Capital Accounts",
                AccountBalance = totalIncomeGl - toatlExpenseGl
            };
            capitalAccounts.Add(CapitalAcctSum);
            return capitalAccounts;
        }

        public List<GlAccountManagement> GetAllLiabilityAccounts()
        {
            var liabilityAccounts = _glAccount.GetMainAccountCat("Liability").ToList();
            var totalSavingsAccount = _customerAccount.GetByAccountType(AccountType.Savings).Sum(a => a.Balance);
            var totalCurrentAccount = _customerAccount.GetByAccountType(AccountType.Current).Sum(a => a.Balance);

            var SavingsLiabilityAccount = new GlAccountManagement
            {
                Name = "Savings Liability Account",
                AccountBalance = totalSavingsAccount
            };

            var CurrentLiabilityAccount = new GlAccountManagement
            {
                Name = "Current Liability Account",
                AccountBalance = totalCurrentAccount
            };

            liabilityAccounts.Add(SavingsLiabilityAccount);
            liabilityAccounts.Add(CurrentLiabilityAccount);

            return liabilityAccounts;
        }

        public List<GlAccountManagement> GetAllIncomeAccounts()
        {
            var incomeAccounts = _glAccount.GetMainAccountCat("Income").ToList();
            return incomeAccounts;
        }

        public List<GlAccountManagement> GetAllExpenseAccounts()
        {
            var expenseAccounts = _glAccount.GetMainAccountCat("Expenses").ToList();
            return expenseAccounts;
        }


    }
}
