using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBApplication.Core.Models;
using CBApplication.Data.Implementations;
using CBApplication.Data.Interfaces;

namespace CBApplicaion.Data.LogicImplementation
{
    public class LoanLogic
    {
        private readonly ILoansRepository _loan = new LoansRepository(new ApplicationDbContext());
        private readonly ICustomerAccountRepository _customer = new CustomerAccountRepository(new ApplicationDbContext());
        private readonly TransactionLogLogic _transaction = new TransactionLogLogic();

        public void SaveLoan(Loans config)
        {
            //saving into the database
            _loan.Add(config);
            _loan.Save(config);
        }

        public void UpdateLoan(Loans config)
        {
            //Updating database vlues
            _loan.Update(config);
        }
        public decimal CalcInterestRate(decimal amount, decimal rate, int duration)
        {
            var result = amount * (rate / 100) * ((decimal)duration / 12);
            return result;
        }

        public DateTime CalcEndDate(int duration)
        {
            var result = DateTime.Now.AddDays(duration * 30);
            return result;
        }

        public decimal CalcInterestDeduction(decimal interest, int duration)
        {
            var result = interest / (duration * 30);
            return result;
        }

        public decimal DailyLonDeductionAndPrincipalRemaining(decimal amount, int duration)
        {
            var result = amount / (duration * 30);
            return result;
        }

        public bool CreditCustomerAccount(decimal amount, int id, CustomerAccount account)
        {
            if (account.AccountType == AccountType.Savings || account.AccountType == AccountType.Current)
            {
                if (id != 0)
                {
                    ApplicationDbContext _context = new ApplicationDbContext();
                    CustomerAccount customerAccount = _customer.Get(id);
                    customerAccount.Balance += amount;

                    _context.Entry(customerAccount).State = EntityState.Modified;
                    _context.SaveChanges();

                }
            }
            else
            {
                    ApplicationDbContext _context = new ApplicationDbContext();
                    CustomerAccount customerAccount = _customer.Get(id);
                    customerAccount.Balance -= amount;

                    _context.Entry(customerAccount).State = EntityState.Modified;
                    _context.SaveChanges();
            }
            //Logging values
            _transaction.LogCustomer(account, amount, TransactionType.Credit);

            return true;
        }

        public bool DebitCustomerAccount(decimal amount, int id, CustomerAccount account)
        {
            if (account.AccountType == AccountType.Savings || account.AccountType == AccountType.Current)
            {
                if (id != 0)
                {
                    ApplicationDbContext _context = new ApplicationDbContext();
                    account.Balance -= amount;
                    _context.Entry(account).State = EntityState.Modified;
                    _context.SaveChanges();
                }
            }
            else //this is a loan account
            {
                if (id != 0)
                {
                    ApplicationDbContext _context = new ApplicationDbContext();
                    account.Balance += amount;
                    _context.Entry(account).State = EntityState.Modified;
                    _context.SaveChanges();
                }
            }

            //Logging values
            _transaction.LogCustomer(account,amount,TransactionType.Debit);
            return true;
        }

        public bool HasUnpaidLoan(long accountNumber)
        {
            var result = _loan.CheckIfPaid(accountNumber);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Loans GetCurrentLoan(long accountNumber)
        {
            var check = _loan.GetCurrentLoan(accountNumber);
            return check;
        }

        public int NumberOfLoanAccounts()
        {
            var loan = _loan.NumberOfLoanAccounts();
            return loan;
        }
    }
}
