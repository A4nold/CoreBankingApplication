using System;
using System.Collections.Generic;
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
    public class AccountConfigurationController : Controller
    {
        private  readonly AccountConfigurationRepository _contextConfig = new AccountConfigurationRepository(new ApplicationDbContext());
        private  readonly CurrentRepository _contextCurrent = new CurrentRepository(new ApplicationDbContext());
        private  readonly LoanRepository _contextLoan = new LoanRepository(new ApplicationDbContext());
        private  readonly GlAccountRepository _contextGlAccount = new GlAccountRepository(new ApplicationDbContext());

        CurrentConfigLogic _logic = new CurrentConfigLogic();
        LoanConfigLogic _loanLogic = new LoanConfigLogic();



        //GET: AccountConfiguration/SavingsConfig
        public ActionResult SavingsConfig()
        {
            SavingsConfig config = _contextConfig.GetSavingsConfig();

            
            SavingsConfigurationViewModels viewModel = new SavingsConfigurationViewModels(config)
            {
                ExpenseAccount = _contextGlAccount.GetExpenseAccount(),
                PayableAccount = _contextGlAccount.GetPayableAccount()

            };

            return View(viewModel);
        }

        //POST: AccountConfiguration/SavingsConfig
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SavingsConfig( SavingsConfig save)
        {
            bool status = false;

            SavingsConfig account = _contextConfig.GetSavingsConfig();

            SavingsConfigurationViewModels viewModel = new SavingsConfigurationViewModels(account)
            {
                ExpenseAccount = _contextGlAccount.GetExpenseAccount()
            };

                var interest = Request.Form["Interest"];
                var balance = Request.Form["Balance"];
                var expenseAcct = Request.Form["Expense"];
                var payableAcct = Request.Form["Payable"];

                save.CreditInterestRate = Math.Round(Convert.ToDecimal(Request.Form["Interest"], CultureInfo.InvariantCulture),4);
                save.MinBalance = Math.Round(Convert.ToDecimal(Request.Form["Balance"], CultureInfo.InvariantCulture),4);
                save.GlAccountId = Convert.ToInt32(Request.Form["Expense"]);
                save.SavingsInterestPayableGlId = Convert.ToInt32(Request.Form["Payable"]);
                save.Id = Convert.ToInt32(Request.Form["Id"]);
                

                    if (save.Id == 0)
                    {
                        save.status = true;
                            
                        _contextConfig.Add(save);

                        _contextConfig.Save(save);

                        TempData["Message"] = "Configurations Added";
                        status = true;

                    }
                    else
                    {
                        var configInDb = _contextConfig.Get(save.Id);

                        configInDb.CreditInterestRate = Math.Round(Convert.ToDecimal(interest, CultureInfo.InvariantCulture),4);
                        configInDb.MinBalance = Math.Round(Convert.ToDecimal(balance, CultureInfo.InvariantCulture),4);
                        configInDb.GlAccountId = Convert.ToInt32(expenseAcct);
                        configInDb.SavingsInterestPayableGlId = Convert.ToInt32(payableAcct);

                        if (interest == null || balance == null || expenseAcct == null)
                        {
                            configInDb.status = false;
                        }

                        configInDb.status = true;

                        _contextConfig.Update(save);

                        TempData["Message"] = "New Configurations Added";
                        status = true;
                    }
                    
                    
            ViewBag.Status = status;
            return RedirectToAction("SavingsConfig");
        }

        //GET: AccountConfiguration/CurrentConfig
        public ActionResult CurrentConfig()
        {
            CurrentConfig config = _contextCurrent.GetCurrentConfig();

            CurrentConfigurationViewModels viewModel = new CurrentConfigurationViewModels(config)
            {
                IncomeAccount = _contextGlAccount.GetIncomeAccount(),
                ExpenseAccount = _contextGlAccount.GetExpenseAccount(),
                PayableAccount = _contextGlAccount.GetPayableAccount()
            };

            return View(viewModel);
        }

        //POST: AccountConfiguration/CurrentConfig
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CurrentConfig(CurrentConfig save)
        {
            bool status = false;

            CurrentConfig config = _contextCurrent.GetCurrentConfig();

            CurrentConfigurationViewModels viewModel = new CurrentConfigurationViewModels(config)
            {
                IncomeAccount = _contextGlAccount.GetIncomeAccount(),
                ExpenseAccount = _contextGlAccount.GetExpenseAccount()
            };


            var interest = Request.Form["Interest"];
            var balance = Request.Form["Balance"];
            var expenseAcct = Request.Form["Expense"];
            var cot = Request.Form["Cot"];
            var incomeAcct = Request.Form["Income"];
            var payable = Request.Form["Payable"];

            save.CreditInterestRate = Math.Round(Convert.ToDecimal(Request.Form["Interest"], CultureInfo.InvariantCulture),4);
            save.MinBalance = Math.Round(Convert.ToDecimal(Request.Form["Balance"], CultureInfo.InvariantCulture),4);
            save.CommissionOnTurnover = Math.Round(Convert.ToDecimal(Request.Form["Cot"], CultureInfo.InvariantCulture),4);
            save.ExpenseGlAccountId = Convert.ToInt32(Request.Form["Expense"]);
            save.IncomeGlAccountId = Convert.ToInt32(Request.Form["Income"]);
            save.CurrentInterestPayableGlId = Convert.ToInt32(Request.Form["Payable"]);
            save.Id = Convert.ToInt32(Request.Form["Id"]);

            if (save.Id == 0)
            {
                

                save.status = true;

                _logic.SaveCurrentConfig(save);

                TempData["Message"] = "Configurations Added";
                status = true;

            }
            else
            {
                var configInDb = _logic.GetCurrent(save.Id);

                configInDb.CreditInterestRate = Math.Round(Convert.ToDecimal(interest, CultureInfo.InvariantCulture),4);
                configInDb.MinBalance = Math.Round(Convert.ToDecimal(balance, CultureInfo.InvariantCulture),4);
                configInDb.ExpenseGlAccountId = Convert.ToInt32(expenseAcct);
                configInDb.CommissionOnTurnover = Math.Round(Convert.ToDecimal(cot, CultureInfo.InvariantCulture),4);
                configInDb.IncomeGlAccountId = Convert.ToInt32(incomeAcct);
                configInDb.CurrentInterestPayableGlId = Convert.ToInt32(payable);

                if (interest == null || balance == null || expenseAcct == null)
                {
                    configInDb.status = false;
                }

                configInDb.status = true;

               _logic.UpdateCurrentConfig(save);

               TempData["Message"] = "New Configurations Added";
                status = true;

            }

           
            ViewBag.Status = status;
            return RedirectToAction("CurrentConfig", "AccountConfiguration");
        }

        //GET: AccountConfiguration/LoanConfig
        public ActionResult LoanConfig()
        {
            var config = _contextLoan.GetLoanConfig();

            var viewModel = new LoanConfigurationViewModels(config)
            {
                IncomeAccount = _contextGlAccount.GetIncomeAccount(),
                RecievableIncomeAccount = _contextGlAccount.GetIncomeAccount()
            };

            return View(viewModel);
        }

        //POST: AccountConfiguration/CurrentConfig
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveLoanConfig(LoanConfig save)
        {
            bool status = false;

            var interest = Request.Form["Interest"];
            var income = Request.Form["Income"];
            var receivableIncome = Request.Form["Receive"];

            save.DebitInterestRate = Math.Round(Convert.ToDecimal(Request.Form["Interest"], CultureInfo.InvariantCulture),4);
            save.IncomeGlAccountId = Convert.ToInt32(Request.Form["Income"]);
            save.LoanInterestReceivableGlId = Convert.ToInt32(Request.Form["Receive"]);
            save.Id = Convert.ToInt32(Request.Form["Id"]);

            if (save.Id == 0)
            {
                save.Status = true;

                _loanLogic.SaveLoanConfig(save);

                TempData["Message"] = "Configurations Added";
                status = true;
            }
            else
            {
                var configInDb = _loanLogic.GetLoan(save.Id);

                configInDb.DebitInterestRate = Math.Round(Convert.ToDecimal(interest, CultureInfo.InvariantCulture),4);
                configInDb.IncomeGlAccountId = Convert.ToInt32(income);
                configInDb.LoanInterestReceivableGlId = Convert.ToInt32(receivableIncome);

                if (interest == null || income == null)
                {
                    configInDb.Status = false;
                }

                configInDb.Status = true;

                _loanLogic.UpdateLoanConfig(save);

                TempData["Message"] = "New Configurations Added";
                status = true;
            }

            
            ViewBag.Status = status;
            return RedirectToAction("LoanConfig", "AccountConfiguration");
        }

        
    }
}
