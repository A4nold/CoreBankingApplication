using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBApplication.Core.Interfaces;
using CBApplication.Core.Models;
using CBApplication.Data.Implementations;
using CBApplication.Data.Interfaces;

namespace CBApplicaion.Data.LogicImplementation
{
    public class TellerPostingLogic
    {
        private readonly ITellerPostingRepository _teller = new TellerPostingRepository(new ApplicationDbContext());
        private readonly ICustomerAccountRepository _customer = new CustomerAccountRepository(new ApplicationDbContext());
        private readonly IGlAccountRepository _glContext = new GlAccountRepository(new ApplicationDbContext());
        private readonly IGlPostingRepository _glPost = new GlPostingRepository(new ApplicationDbContext());
        
        private readonly ICurrentRepository _current = new CurrentRepository(new ApplicationDbContext());
        ApplicationDbContext _context = new ApplicationDbContext();
        private readonly TellerRepository _tillAccount = new TellerRepository(new ApplicationDbContext());
        private readonly TransactionLogLogic _transaction = new TransactionLogLogic();

        private readonly CotPostLogic _cotLogic = new CotPostLogic();
        
        private LoanLogic _loanLogic = new LoanLogic();

        public decimal TellerBalance(int userId)
        {
            var tillAcct = _teller.GetTillUserId(userId).AccountManagementId;
            var result = _glContext.GetTillAccount(tillAcct).AccountBalance;
            return result;
        }

        public void SavePost(TellerPosting savePost)
        {
            //saving into the database
            _teller.Add(savePost);
            _teller.Save(savePost);
        }

        public void UpdatePost(TellerPosting savePost)
        {
            //Updating database values
            _teller.Save(savePost);
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
            _transaction.LogCustomer(account, amount, TransactionType.Debit);
            return true;
        }

        public bool DebitGlAccount(decimal amount, GlAccountManagement account)
        {
            var acctCode = GetFirstAcc(account);
            switch (acctCode)
            {
                case "1":
                case "5":
                    account.AccountBalance += amount;
                    _context.Entry(account).State = EntityState.Modified;
                    _context.SaveChanges();
                    break;
                case "2":
                case "3":
                case "4":
                    account.AccountBalance -= amount;
                    _context.Entry(account).State = EntityState.Modified;
                    _context.SaveChanges();
                    break;
            }
            //Logging Values
            _transaction.Log(account, amount, TransactionType.Debit);
            return true;
        }

        public bool CreditGlAccount(decimal amount, GlAccountManagement account)
        {
            var acctCode = GetFirstAcc(account);
            switch (acctCode)
            {
                case "1":   
                case "5":
                    account.AccountBalance -= amount;
                    _context.Entry(account).State = EntityState.Modified;
                    _context.SaveChanges();
                    break;
                case "2":
                case "3":
                case "4":
                    account.AccountBalance += amount;
                    _context.Entry(account).State = EntityState.Modified;
                    _context.SaveChanges();
                    break;
            }
            //Logging Values
            _transaction.Log(account,amount,TransactionType.Credit);
            return true;
        }

        public bool CreditLoanAccount(Loans account, decimal amount)
        {
            try
            {
                account.Amount -= amount;
                _loanLogic.SaveLoan(account);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public decimal calcCOT(decimal cot, decimal amount)
        {
            var result = (amount / 1000) * cot;
            return result;
        }

        public bool CheckWithdrawalPossible(decimal cot, decimal accountBalance, decimal amount)
        {
            var calcCot = calcCOT(cot, amount);

            if (accountBalance < (calcCot + amount))
            {
                return false;
            }

            return true;
        }

        public string GetFirstAcc(GlAccountManagement gl)
        {
            var result = gl.AccCode.ToString()[0].ToString();
            return result;
        }

        public bool BuyCash(decimal amount, int userId)
        {
            //Get teller till account
            var tillAccountId = _tillAccount.GetUserId(userId).AccountManagementId;
            var tillGlAccount = _glContext.Get(tillAccountId);

            //Get Vault Gl Account
            var VaultGl = _glContext.GetVault();
            

            var check = (VaultGl.AccountBalance < tillGlAccount.AccountBalance)
                ? "You Cannot Buy this amount"
                : "false";

            if (check == "false")
            {
                DebitGlAccount(amount, tillGlAccount);
                CreditGlAccount(amount, VaultGl);

                //Log Transaction
                GlPosting post = new GlPosting()
                {
                    Amount = amount,
                    DatePosted = DateTime.Now,
                    CreditAccountId = VaultGl.Id,
                    DebitAccountId = tillGlAccount.Id,
                    CreditAccountNarration = "I sold to the till",
                    DebitAccountNarration = "I bought from the vault",
                    UserId = userId
                };

                _glPost.Add(post);
                _glPost.Save(post);

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SellCash(decimal amount, int userId)
        {
            //Get teller till account
            var tillAccountId = _tillAccount.GetUserId(userId).AccountManagementId;
            var tillGlAccount = _glContext.Get(tillAccountId);

            //Get Vault Gl
            var VaultGl = _glContext.GetVault();

            var check = (amount > tillGlAccount.AccountBalance)
                ? "You Cannot Sell this Amount"
                : "false";

            if (check == "false")
            {
                DebitGlAccount(amount, VaultGl);
                CreditGlAccount(amount, tillGlAccount); 

                 //Log Transaction
                 GlPosting post = new GlPosting()
                {
                    Amount = amount,
                    DatePosted = DateTime.Now,
                    CreditAccountId = tillGlAccount.Id,
                    DebitAccountId = VaultGl.Id,
                    CreditAccountNarration = "I sold to the vault",
                    DebitAccountNarration = "I bought from the till",
                    UserId = userId
                };

                _glPost.Add(post);
                _glPost.Save(post);

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Deposit(decimal amt, int custId, int userId,TellerPosting tlPosting,DateTime date)
        {
            //Get teller till account
            var tillAccountId = userId;
            var tillAccount = _tillAccount.GetUserId(tillAccountId);
            var tillGlAccountId = tillAccount.AccountManagementId;
            var tellerGlAccount = _glContext.Get(tillGlAccountId);

            //Get Customer Account
            var customerAccount = _customer.Get(custId);
            var amount = amt;
            var customerId = custId;
            var datePosted = date;


            if (tlPosting.Type == PostingType.Deposit)
            {
                //Performing transactions
                CreditCustomerAccount(amount, customerId, customerAccount);
                DebitGlAccount(amount, tellerGlAccount);


                //Saving teller posting
                SavePost(tlPosting);
                return true;

            }

            return true;
        }

        public bool Withdrawal(decimal amount, int custId, int userId, TellerPosting tlPosting,DateTime date)
        {
            //Get teller till account
            var tillAccountId = userId;
            var tillAccount = _tillAccount.GetUserId(tillAccountId);
            var tillGlAccountId = tillAccount.AccountManagementId;
            var tellerGlAccount = _glContext.Get(tillGlAccountId);

            //Get Customer Account
            var CustomerAccount = _customer.Get(custId);
            var Amount = amount;
            var CustomerId = custId;
            var DatePosted = date;

            

            if (CustomerAccount.AccountType == AccountType.Savings)
            {
                DebitCustomerAccount(Amount, custId, CustomerAccount);
                CreditGlAccount(Amount, tellerGlAccount);

                //Saving teller posting
                SavePost(tlPosting);

                return true;
            }

            if (CustomerAccount.AccountType == AccountType.Current)
            {
                //Getting the Income COT Account
                var CurrentIncomeGl = _glContext.Get(_current.GetFirst().IncomeGlAccountId);
                

                //Getting Cot Values from the current account configuration
                var currentConfig = _current.GetFirst();
                var cot = currentConfig.CommissionOnTurnover;

                var cotValue = calcCOT(cot, Amount);

                var isPossible = CheckWithdrawalPossible(cot, CustomerAccount.Balance, Amount);

                if (isPossible)
                {
                    DebitCustomerAccount(Amount + cotValue, CustomerId, CustomerAccount);
                    CreditGlAccount(Amount, tellerGlAccount);
                    CreditGlAccount(cotValue, CurrentIncomeGl);

                    //saving into database
                    SavePost(tlPosting);

                    //Loging values into database
                    var CotLog = new COTPost()
                    {
                        CustomerAccountId = CustomerId,
                        GlAccountId = tillAccountId,
                        Amount = Amount,
                        WhenPosted = DatePosted
                    };


                    CustomerAccount.CotAccured += cotValue;

                    //Adding into customerAccount Entity
                    _context.Entry(CustomerAccount).State = EntityState.Modified;
                    _context.SaveChanges();


                    //Adding into COTPost Entity
                    _cotLogic.SavePost(CotLog);

                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public int NumberOfTillAccounts()
        {
            //Number of till accounts
            var tellers = _teller.NumberOfTillAccounts();
            return tellers;
        }
    }
}
