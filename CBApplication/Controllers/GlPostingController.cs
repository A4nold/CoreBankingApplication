using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    public class GlPostingController : Controller
    {
        private readonly GlPostingRepository _contextGl = new GlPostingRepository(new ApplicationDbContext());
        private readonly GlAccountRepository _contextAccount = new GlAccountRepository(new ApplicationDbContext());

        readonly GlPostingLogic _logic = new GlPostingLogic();

        //GET: GlPosting/Create
        public ActionResult Create()
        {
            GlPostingViewModels viewModel = new GlPostingViewModels()
            {
                CreditAccount = _contextAccount.GetAll(),
                DebitAccount = _contextAccount.GetAll()
            };

            return View(viewModel);
        }

        //POST: GlPosting/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GlPosting savePost)
        {
            bool status = false;
            string message = "";

            GlPostingViewModels viewModel = new GlPostingViewModels()
            {
                CreditAccount = _contextAccount.GetAll(),
                DebitAccount = _contextAccount.GetAll()
            };

            try
            {
                //getting values from form
                savePost.Amount = Convert.ToDecimal(Request.Form["Amount"], CultureInfo.InvariantCulture);
                savePost.DebitAccountNarration = Request.Form["DebitNarration"];
                savePost.DebitAccountId = Convert.ToInt32(Request.Form["Debit"]);
                savePost.CreditAccountNarration = Request.Form["CreditNarration"];
                savePost.CreditAccountId = Convert.ToInt32(Request.Form["Credit"]);
                savePost.UserId = Convert.ToInt32(Request.Form["Id"]);
                savePost.DatePosted = DateTime.Now;

                var checkingAccounts = _logic.CheckAccount(savePost.DebitAccountId, savePost.CreditAccountId);

                if (checkingAccounts)
                {
                    ModelState.AddModelError("AccountEqual", "Accounts are the same, Please check again");
                    return View(viewModel);
                }

                var creditAcct = _contextAccount.Get(savePost.CreditAccountId);
                var debitAcct = _contextAccount.Get(savePost.DebitAccountId);
                var amount = savePost.Amount;

                //Validating amount in account
                if (creditAcct.Name.ToLower().Contains("till")|| creditAcct.Name.ToLower().Contains("vault"))
                {
                    if (creditAcct.AccountBalance < amount)
                    {
                        ModelState.AddModelError("AccountEqual", "You are posting an insufficient amount");
                        return View(viewModel);
                    }
                }

                //getting account code used
                var creditacctcode = creditAcct.AccCode.ToString()[0].ToString();
                var debitacctcode = debitAcct.AccCode.ToString()[0].ToString();

                //Posting logic
                _logic.CreditPost(amount, creditAcct, creditacctcode);
                _logic.DebitPost(amount, debitAcct, debitacctcode);


                //save post into database
                _logic.SavePost(savePost);
                _contextAccount.Update(creditAcct);
                _contextAccount.Update(debitAcct);


                message = "Post Complete";
                status = true;

                ViewBag.Message = message;
                ViewBag.Status = status;
                return View(viewModel);
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

        }

        //GET: GlPosting/Index
        public ActionResult Index()
        {
            var posts = _contextGl.GetAllPosting().ToList();
            return View(posts);

        }
    }
}
