using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBApplication.Core.Models;
using CBApplication.Data.Implementations;

namespace CBApplicaion.Data.LogicImplementation
{
    public class EodLogic
    {
        private readonly AccountConfigurationRepository _savings = new AccountConfigurationRepository(new ApplicationDbContext());
        private readonly CurrentRepository _current = new CurrentRepository(new ApplicationDbContext());
        private readonly GlAccountRepository _glAccount = new GlAccountRepository(new ApplicationDbContext());
        private readonly LoanRepository _loan = new LoanRepository(new ApplicationDbContext());
        private readonly BankConfigRepository _bankConfig = new BankConfigRepository(new ApplicationDbContext());
        private readonly LoansRepository _loanAccount = new LoansRepository(new ApplicationDbContext());
        private readonly CustomerAccountRepository _customerAccount = new CustomerAccountRepository(new ApplicationDbContext());

        private TellerPostingLogic _post = new TellerPostingLogic();

        private DateTime financialDate;
        private BankConfig config;

        public EodLogic()
        {
            config = _bankConfig.GetFirst();
            financialDate = config.FinancialDate;
        }
        
        private int[] daysInMonth = new int[12] {31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};

        public string RunEOD()
        {

            
            try
            {
                LoanAccountInterestRate();
                AccountInterestRate();

                config.FinancialDate = financialDate.AddDays(1);
                _bankConfig.Save(config);
                return "Success";
            }
            catch (Exception) {
                return "Error Occured while running EOD";
            }
        }

        public void AccountInterestRate()
        {
            var day = financialDate.Day;
            var month = financialDate.Month;
            var daysRemaining = 0;
            var savingsConfig = _savings.GetFirst();
            var currentConfig = _current.GetFirst();

            var savingsAccount = _customerAccount.GetByAccountType(AccountType.Savings);
            var currentAccount = _customerAccount.GetByAccountType(AccountType.Current);

            if (savingsAccount != null)
            {
                var savingsInterestGl = _glAccount.Get(savingsConfig.GlAccountId);
                var savingsInterestPayableGl = _glAccount.Get(savingsConfig.SavingsInterestPayableGlId);
                decimal todaysInterest = 0;

                foreach (var account in savingsAccount)
                {
                    daysRemaining = daysInMonth[month - 1] - day + 1;

                    decimal interestRemainingforTheMonth =
                        account.Balance * (savingsConfig.CreditInterestRate / 100) *
                        ((decimal)daysRemaining / daysInMonth[month - 1]);

                    if (daysRemaining != 0)
                    {
                        todaysInterest = interestRemainingforTheMonth / daysRemaining;
                    }
                    
                    account.InterestGot += todaysInterest;

                    _post.DebitGlAccount(todaysInterest, savingsInterestGl);
                    _post.CreditGlAccount(todaysInterest, savingsInterestPayableGl);
                    _customerAccount.Save(account);
                }

                if (day == daysInMonth[month - 1])
                {
                    foreach (var account in savingsAccount)
                    {
                        _post.DebitGlAccount(account.InterestGot, savingsInterestPayableGl);
                        _post.CreditCustomerAccount(account.InterestGot, account.Id, account);

                        account.InterestGot = 0;
                        _customerAccount.Save(account);
                    }
                }
            }
            if (currentAccount != null)
            {
                var currentInterestGl = _glAccount.Get(currentConfig.ExpenseGlAccountId);
                var currentInterestPayableGl = _glAccount.Get(currentConfig.CurrentInterestPayableGlId);

                var COTGl = _glAccount.Get(currentConfig.IncomeGlAccountId);

                foreach (var account in currentAccount)
                {
                    decimal todaysInterest = 0;
                    daysRemaining = daysInMonth[month - 1] - day + 1;

                    decimal interestRemainingforTheMonth =
                        account.Balance * (savingsConfig.CreditInterestRate / 100) *
                        ((decimal)daysRemaining / daysInMonth[month - 1]);
                    if (daysRemaining != 0)
                    {
                         todaysInterest = interestRemainingforTheMonth / daysRemaining;

                    }

                    account.InterestGot += todaysInterest;

                    _post.DebitGlAccount(todaysInterest, currentInterestGl);
                    _post.CreditGlAccount(todaysInterest, currentInterestPayableGl);
                    _customerAccount.Save(account);
                }

                if (day == daysInMonth[month - 1])
                {
                    foreach (var account in currentAccount)
                    {
                        //remove cot accrued first
                        _post.DebitCustomerAccount(account.CotAccured, account.Id, account);
                        _post.CreditGlAccount(account.CotAccured, COTGl);
                        account.CotAccured = 0;
                        _customerAccount.Save(account);

                        //add interest for current account
                        _post.DebitGlAccount(account.InterestGot, currentInterestPayableGl);
                        _post.CreditCustomerAccount(account.InterestGot, account.Id, account);

                        account.InterestGot = 0;

                        _customerAccount.Save(account);
                    }
                }
            }
        }

        //public void CurrentAccountCotRate()
        //{
        //    var month = financialDate.Month;
        //    var currentAccounts = _customerAccount.GetByAccountType(AccountType.Current);
        //    var currentConfig = _current.GetFirst();

        //    var COTGl = _glAccount.Get(currentConfig.IncomeGlAccountId);
        //    if (currentAccounts != null)
        //    {
        //        if (financialDate.Day == daysInMonth[month - 1])
        //        {
        //            foreach (var account in currentAccounts)
        //            {
        //                _post.DebitCustomerAccount(account.CotAccured, account.Id, account);
        //                _post.CreditGlAccount(account.CotAccured, COTGl);

        //                account.CotAccured = 0;
        //                _customerAccount.Save(account);
        //            }
        //        }
        //    }
        //}

        public void LoanAccountInterestRate()
        {
            var loanConfig = _loan.GetFirst();
            var loanAcct = _loanAccount.GetAll();

            if (loanAcct != null)
            {

                var interestIncomeGlAccount = _glAccount.Get(loanConfig.IncomeGlAccountId);
                

                foreach (var account in loanAcct)
                {
                    var loanAccountId = _customerAccount.GetByAccountNumber(account.AccountNumber);
                    var customerAccount = _customerAccount.Get(account.CustomerAccountId);

                    if (account.DurationInMonths * 30 != account.DaysCount)
                    {
                        //payment of interest
                        _post.CreditGlAccount(account.InterestDeduction, interestIncomeGlAccount);
                        _post.DebitCustomerAccount(account.InterestDeduction, account.CustomerAccountId, customerAccount);

                        account.InterestRemaining -= account.InterestDeduction;


                        //paying daily loan back
                        _post.DebitCustomerAccount(account.DailyLoanDeduction, account.CustomerAccountId, customerAccount);
                        _post.CreditCustomerAccount(account.DailyLoanDeduction, loanAccountId.Id, loanAccountId);

                        account.LoanBalance -= account.DailyLoanDeduction;

                        _loanAccount.Save(account);
                    }

                    if (account.DurationInMonths * 30 == account.DaysCount)
                    {
                        account.Status = false;
                        account.DaysCount = 0;
                    }

                    //increases days if loan payment not reached
                    if (account.DurationInMonths * 30 != account.DaysCount)
                    {
                        account.DaysCount++;
                    }

                    _loanAccount.Update(account);
                }
            }
        }
    }
}
