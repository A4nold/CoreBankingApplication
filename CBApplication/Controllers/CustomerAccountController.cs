using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using CBApplicaion.Data.LogicImplementation;
using CBApplication.Core.Models;
using CBApplication.Core.ViewModels;
using CBApplication.Data.Implementations;

namespace CBApplication.Controllers
{
    [RestrictNonAdmins]
    [CheckSession]
    public class CustomerAccountController : Controller
    {
        private  readonly CustomerAccountRepository _contextCustomerAccount = new CustomerAccountRepository(new ApplicationDbContext());
        private  readonly BranchRepository _contextBranch = new BranchRepository(new ApplicationDbContext());
        private  readonly CustomerRepository _contextCustomer = new CustomerRepository(new ApplicationDbContext());
        private readonly LoansRepository _loan = new LoansRepository(new ApplicationDbContext());

        private readonly CustomerAccountLogic _logic = new CustomerAccountLogic();
        private readonly LoanConfigLogic _loanContextLogic = new LoanConfigLogic();
        private readonly LoanLogic _loanLogic = new LoanLogic();
        


        //GET: CustomerAccount/Create
        public ActionResult Create(int id)
        {
            var cust = _contextCustomer.Get(id);

            var viewmodel = new CustomerAccountViewModels
            {
                Branches = _contextBranch.GetAll(),
                Customers = cust
            };

            return View(viewmodel);
        }

        //POST: CustomerAccount/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerAccount customeraccount)
        {
            customeraccount.AccountType = (AccountType) Convert.ToInt32(Request.Form["Type"]);
            customeraccount.branchName = Request.Form["Branch"];
            var accountType = customeraccount.AccountType.ToString();
            customeraccount.AccountNumber =
                _logic.generateAccountNumber(accountType, customeraccount.customerId);
            customeraccount.AccountName = Request.Form["AcctName"];
            customeraccount.customerId = Request.Form["customerId"];
            var id = Convert.ToInt32(Request.Form["customerId"]);
            try
            {
                if (ModelState != null)
                {
                    if (customeraccount.AccountType != 0)
                    {
                        //Adding values into database
                        _contextCustomerAccount.Add(customeraccount);

                        //Saving Information into database
                        _contextCustomerAccount.Save(customeraccount);

                        return RedirectToAction("Index", "CustomerAccount");
                    }

                    if (customeraccount.AccountType == 0)
                    {
                        ModelState.AddModelError("AcctEmpty", "Account Type Not Selected");
                    }
                }

                
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                var fullErrorMessage = string.Join("; ", errorMessages);
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }

            var cust = _contextCustomer.Get(id);

            var viewmodel = new CustomerAccountViewModels
            {
                Branches = _contextBranch.GetAll(),
                Customers = cust
            };

            return View(viewmodel);
        }

        //GET: CustomerAccount/Index
        public ActionResult Index()
        {
            var accountList = _contextCustomerAccount.GetAll();
            return View(accountList);
        }

        //GET: CustomerAccount/Edit/{id}
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                CustomerAccount account = _contextCustomerAccount.Get(id);
                

                var viewmodel = new CustomerAccountViewModels
                {
                    Branches = _contextBranch.GetAll(),
                    CustomerAccount = account
                };

                if (account != null)
                {
                    return View(viewmodel);
                }

                return HttpNotFound();

            }
            return HttpNotFound();
        }

        //POST: CustomerAccount/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerAccount account)
        {
            if (account.Id == 0)
            {
                _contextCustomerAccount.Save(account);
            }
            else
            {
                var accountInDb = _contextCustomerAccount.Get(account.Id);
                accountInDb.AccountName = Request.Form["AcctName"];
                accountInDb.branchName = Request.Form["Branch"];

                _contextCustomerAccount.Update(account);
            }

            return RedirectToAction("Index", "CustomerAccount");
        }

        //GET: CustomerAccount/Close/{id}
        public ActionResult Close(int? id)
        {
            if (id != null)
            {
                CustomerAccount account = _contextCustomerAccount.Get(id);

                var viewmodel = new CustomerAccountViewModels
                {
                    CustomerAccount = account
                };

                if (account != null)
                {
                    return View(viewmodel);
                }

                return HttpNotFound();
            }
            return HttpNotFound();
        }

        //POST: CustomerAccount/Close/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Close(CustomerAccount account)
        {
            if (account.Id == 0)
            {
                _contextCustomerAccount.Save(account);
            }
            else
            {
                var accountInDb = _contextCustomerAccount.Get(account.Id);
                accountInDb.isClosed = true;
                _contextCustomerAccount.Update(account);
            }
            return RedirectToAction("Index", "CustomerAccount");
        }

        //GET: CustomerAccount/Open/{id}
        public ActionResult Open(int? id)
        {
            if (id != null)
            {
                CustomerAccount account = _contextCustomerAccount.Get(id);

                var viewmodel = new CustomerAccountViewModels
                {
                    CustomerAccount = account
                };

                if (account != null)
                {
                    return View(viewmodel);
                }

                return HttpNotFound();
            }
            return HttpNotFound();
        }

        //POST: CustomerAccount/Open/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Open(CustomerAccount account)
        {
            if (account.Id == 0)
            {
                _contextCustomerAccount.Save(account);
            }
            else
            {
                var accountInDb = _contextCustomerAccount.Get(account.Id);
                accountInDb.isClosed = false;
                _contextCustomerAccount.Update(account);
            }
            return RedirectToAction("Index", "CustomerAccount");
        }

        //GET: CustomerAccount/Lien/{id}
        public ActionResult Lien(int? id)
        {
            if (id != null)
            {
                CustomerAccount account = _contextCustomerAccount.Get(id);

                var viewmodel = new CustomerAccountViewModels
                {
                    CustomerAccount = account
                };

                if (account != null)
                {
                    return View(viewmodel);
                }

                return HttpNotFound();
            }
            return HttpNotFound();
        }

        //POST: CustomerAccount/Lien/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Lien(CustomerAccount account)
        {
            bool status = false;
            string message = "";

            if (account.Id == 0)
            {
                _contextCustomerAccount.Save(account);
            }
            else
            {
                var accountInDb = _contextCustomerAccount.Get(account.Id);
                accountInDb.CurrentLien = Convert.ToInt16(Request.Form["Lien"], CultureInfo.InvariantCulture);

                _contextCustomerAccount.Update(account);

                message = "Lien Added";
                status = true;

                ViewBag.Message = message;
                ViewBag.Status = status;

                return View();
            }

            return View();
        }

        //POST: CustomerAccount/TakeLoan/{id}
        public ActionResult TakeLoan(int? id)
        {
            if (id != null)
            {
                CustomerAccount account = _contextCustomerAccount.Get(id);

                if (account != null)
                {
                    var loan = new Loans
                    {
                        CustomerAccount = account
                    };

                    return View(loan);
                }

                return HttpNotFound();
            }
            return HttpNotFound();
        }

        //POST: CustomerAccount/TakeLoan/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TakeLoan(Loans loan)
        {
            var findCustomer = _contextCustomerAccount.Get(loan.Id);
            var loanStatus = _loanLogic.HasUnpaidLoan(findCustomer.AccountNumber);
            
            //var culture = new CultureInfo("ig-ng");
            loan.Amount = Convert.ToDecimal(Request.Form["LoanAmount"], CultureInfo.InvariantCulture);
            loan.Narration = Request.Form["Narration"];
            loan.DurationInMonths = Convert.ToInt32(Request.Form["Duration"]);
            var custId = Request.Form["Id"];
            var id = Request.Form["customerId"];


            if (loanStatus)
            {
                TempData["Message"] = "Customer has an Unpaid loan";
                return RedirectToAction("TakeLoan");
            }
            else
            {


                var customerAccount = _contextCustomerAccount.Get(Convert.ToInt32(custId));
                loan.CustomerAccountId = customerAccount.Id;
                loan.Name = customerAccount.AccountName;
                loan.CustomerAccountNumber = customerAccount.AccountNumber;
                loan.AccountNumber = _logic.generateAccountNumber("Loan", id);


                var amount = loan.Amount;
                var duration = loan.DurationInMonths;

                var getAll = _loanContextLogic.GetAll();

                if (getAll.Count() != 0)
                {
                    var resultValue = getAll.Single();
                    loan.InterestRate = resultValue.DebitInterestRate;
                    loan.LoanBalance = loan.Amount;
                    loan.Id = 0;
                    loan.StartDate = DateTime.Now;
                    loan.Status = true;
                    loan.DaysCount = 0;
                    loan.EndDate = _loanLogic.CalcEndDate(loan.DurationInMonths);
                    loan.Interest = Math.Round(_loanLogic.CalcInterestRate(amount, loan.InterestRate, duration), 4);
                    loan.InterestRemaining = loan.Interest;
                    loan.InterestDeduction =
                        Math.Round(_loanLogic.CalcInterestDeduction(loan.Interest, loan.DurationInMonths), 4);
                    loan.DailyLoanDeduction =
                        Math.Round(
                            _loanLogic.DailyLonDeductionAndPrincipalRemaining(loan.Amount, loan.DurationInMonths), 4);
                    loan.PrincipalRemaining = Math.Round(loan.Amount / (loan.DurationInMonths * 30), 4);


                }

                _loanLogic.SaveLoan(loan);

                //Create customer loan account
                var customerLoanAcct = new CustomerAccount
                {
                    customerId = customerAccount.customerId,
                    AccountName = customerAccount.AccountName,
                    AccountNumber = loan.AccountNumber,
                    branchName = customerAccount.branchName,
                    Balance = 0,
                    AccountType = AccountType.Loan,
                    isClosed = false
                };

                _logic.SavePost(customerLoanAcct);
                var loanAccountId = _contextCustomerAccount.GetByAccountNumber(loan.AccountNumber);

                //Crediting customer Account with loan taken
                _loanLogic.CreditCustomerAccount(amount, loan.CustomerAccountId, customerAccount);
                _loanLogic.DebitCustomerAccount(amount, loanAccountId.Id, loanAccountId);


                ViewBag.Message = "Loan Application Successful";
                ViewBag.Status = true;
            }

            //return View();
            return RedirectToAction("LoanIndex", "CustomerAccount");

        }

        //GET: CustomerAccount/LoanIndex
        public ActionResult LoanIndex()
        {
            var loanList = _loan.GetAll();
            return View(loanList);
        }

        // GET: Customer/Details
        public ActionResult Details(int? id)
        {
            //Loans loans = new Loans();
            //var loan = _loan.GetCurrentLoanById(loans.Id);
            var cust = _contextCustomerAccount.Get(id);
            if (id == null)
            {
                return HttpNotFound();
            }

            if (cust == null)
            {
                return HttpNotFound();
            }

            return View(cust);
        }
    }
}
