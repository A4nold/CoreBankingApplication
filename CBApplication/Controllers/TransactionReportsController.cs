using System;
using System.Collections.Generic;
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
    public class TransactionReportsController : Controller
    {
        private readonly TransactionLogRepository _context = new TransactionLogRepository(new ApplicationDbContext());

        private readonly ReportsLogic _logic = new ReportsLogic();

        public ActionResult BalanceSheet()
        {
            var assetAccount = _logic.GetAllAssetAccounts();
            var liabilityAccount = _logic.GetAllLiabilityAccounts();
            var capitalAccount = _logic.GetAllCapitalAccounts();
            var totalAssets = assetAccount.Sum(a => a.AccountBalance);
            var totalLiabilities = liabilityAccount.Sum(a => a.AccountBalance);
            var totalCapital = capitalAccount.Sum(a => a.AccountBalance);
            var cLsum = totalCapital + totalLiabilities;

            var viewModel = new BalanceSheetViewModels
            {
                AssetsGL = assetAccount,
                CapitalGl = capitalAccount,
                LiabiliyGl = liabilityAccount,
                AssetSum = Math.Round(totalAssets,2),
                CapitalSum = Math.Round(totalCapital,2),
                LiabilitySum = Math.Round(totalLiabilities,2),
                CLSum = Math.Round(cLsum,2)
            };

            return View(viewModel);
        }

        public ActionResult ProfitAndLoss()
        {
            var incomeGl = _logic.GetAllIncomeAccounts();
            var expenseGl = _logic.GetAllExpenseAccounts();
            var totalIncomeAccounts = _logic.GetAllIncomeAccounts().Sum(a => a.AccountBalance);
            var totalExpenseAccounts = _logic.GetAllExpenseAccounts().Sum(a => a.AccountBalance);

            var profit = totalIncomeAccounts - totalExpenseAccounts;

            var viewModel = new ProfitandLossViewModels
            {
                IncomeGL = incomeGl,
                ExpenseGl = expenseGl,
                totalIncome = Math.Round(totalIncomeAccounts,2),
                totalExpense = Math.Round(totalExpenseAccounts,2),
                profit = Math.Round(profit,2)
            };

            return View(viewModel);
        }

        public ActionResult TrialBalance()
        {
            var allLogs = _context.GetAllLogs().ToList();
            var viewModels = new List<TrialBalanceViewModel>();

            foreach (var transaction in allLogs)
            {
                var model = viewModels.FirstOrDefault(t => t.AccountCode == transaction.AccountCode);

                if (model == null)
                {
                    model = new TrialBalanceViewModel
                    {
                        AccountCode = transaction.AccountCode,
                        AcctName = transaction.Name,
                        Category = transaction.MainAccountCategory,
                        totalDebit = transaction.Type == TransactionType.Debit ? transaction.Amount : 0,
                        totalCredit = transaction.Type == TransactionType.Credit ? transaction.Amount : 0
                    };
                    viewModels.Add(model);
                }
                else
                {
                    var amount = transaction.Amount;
                    if (transaction.Type == TransactionType.Debit)
                    {
                        model.totalDebit += amount;
                    }
                    else
                    {
                        model.totalCredit += amount;
                    }
                }
            }

            ViewBag.DebitTotal = viewModels.Sum(c => c.totalDebit);
            ViewBag.CreditTotal = viewModels.Sum(c => c.totalCredit);


            return View(viewModels);
        }
    }
}
