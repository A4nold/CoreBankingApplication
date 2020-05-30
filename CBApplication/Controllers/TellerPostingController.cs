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
using CBApplication.Data.Implementations;

namespace CBApplication.Controllers
{
    public class TellerPostingController : Controller
    {
        private readonly TellerPostingRepository _contextPosting = new TellerPostingRepository(new ApplicationDbContext());
        private readonly CustomerAccountRepository _contextAccount = new CustomerAccountRepository(new ApplicationDbContext());
        private readonly TellerRepository _tillAccount = new TellerRepository(new ApplicationDbContext());
        private readonly GlAccountRepository _glContext = new GlAccountRepository(new ApplicationDbContext());

        private readonly ApplicationDbContext _context;

        private TellerPostingLogic _logic = new TellerPostingLogic();
        readonly CurrentConfigLogic _config = new CurrentConfigLogic();

        //public TellerPostingController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}

        //GET: TellerPosting/Index
        public ActionResult Index()
        {
            var result = _contextAccount.GetActiveAccounts();
            return View(result);
        }

        //GET: TellerPosting/SavePost
        public ActionResult Save(int id)
        {
            ViewBag.CustomerId = id;

            //var result = _contextPosting.GetAll();
            return View();
        }

        //POST: TellerPosting/SavePost
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(TellerPosting tellerPosting)
        {
            bool status = false;
            string message = "";

            var checkConfig = _config.checkConfig();

            if (checkConfig)
            {
                //Getting values from form
                tellerPosting.Amount = Convert.ToDecimal(Request.Form["Amount"], CultureInfo.InvariantCulture);
                tellerPosting.Narration = Request.Form["Narration"];
                tellerPosting.DateOfPosting = DateTime.Now;
                tellerPosting.Type = (PostingType)Convert.ToInt32(Request.Form["Type"]);
                tellerPosting.CustomerId = Convert.ToInt32(Request.Form["Id"]);
                tellerPosting.UserId = Convert.ToInt32(Request.Form["tellerId"]);

                var customerAccount = _contextAccount.Get(tellerPosting.CustomerId);
                var amount = tellerPosting.Amount;
                var customerId = tellerPosting.CustomerId;
                var userId = tellerPosting.UserId;
                var datePosted = tellerPosting.DateOfPosting;

                var tillAccountId = Convert.ToInt32(Session["ID"]);
                var tillAccount = _tillAccount.GetUserId(tillAccountId);
                var tillGlAccountId = tillAccount.AccountManagementId;
                var tellerGlAccount = _glContext.Get(tillGlAccountId);

                if (tellerPosting.Type == PostingType.Withdrawal)
                {
                    if (tellerGlAccount.AccountBalance < tellerPosting.Amount)
                    {
                        ModelState.AddModelError("Account", "Insufficient funds in till account");
                        return View();
                    }

                    if (customerAccount.Balance < tellerPosting.Amount)
                    {
                        ModelState.AddModelError("Account", "Insufficient funds in customer account");
                        return View();
                    }

                    if (customerAccount.Balance < amount + customerAccount.CurrentLien)
                    {
                        ModelState.AddModelError("Account", "Cannot Withdraw This Amount");
                        return View();
                    }

                    var withdraw = _logic.Withdrawal(amount, customerId, userId, tellerPosting, datePosted);

                    if (withdraw != true)
                    {
                        ViewBag.Message = "Withdraw Failed";
                        ViewBag.Status = false;
                        return View();
                    }


                    message = "Withdrawal Successful";
                    status = true;

                    ViewBag.Message = message;
                    ViewBag.Status = status;
                    return RedirectToAction("Index", "TellerPosting");
                }


                var deposit = _logic.Deposit(amount, customerId, userId, tellerPosting, datePosted);

                if (deposit != true)
                {
                    ViewBag.Message = "Deposit Failed";
                    ViewBag.Status = false;
                    return View();
                }


                message = "Deposit Successful";
                status = true;

                ViewBag.Message = message;
                ViewBag.Status = status;

            }
            else
            {
                TempData["Error"] = "A Configuration is not set";
                return View();
            }

            return RedirectToAction("Index", "TellerPosting");
        }

        //GET: TellerPosting/ViewPost
        public ActionResult ViewAll()
        {
            var tillId = Convert.ToInt32(Session["Id"]);
            ViewBag.tillBalance = _logic.TellerBalance(tillId);

            var tellerPosts = _contextPosting.GetTellerPosting(tillId).ToList();
            return View(tellerPosts);
        }

        //GET: TellerPosting/BuyCash
        public ActionResult BuyCash()
        {
            var id = Convert.ToInt32(Session["Id"]);
            ViewBag.tillBalance = _logic.TellerBalance(id);

            return View();
        }

        //POST: TellerPosting/BuyCash
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BuyCash(int id)
        {
            
            var userId = Convert.ToInt32(Session["Id"]);
            var amount = Convert.ToDecimal(Request.Form["Amount"], CultureInfo.InvariantCulture);

            var Buy = _logic.BuyCash(amount, userId);

            if (Buy != true)
            {
                ViewBag.Message = "You Cannot Buy This Amount";
                ViewBag.Status = false;
                return View();
            }

            ViewBag.Message = "Cash Bought Successfully";
            ViewBag.Status = true;
            return RedirectToAction("ViewAll", "TellerPosting");
        }

        //GET: TellerPosting/SellCash
        public ActionResult SellCash()
        {
            var id = Convert.ToInt32(Session["Id"]);
            ViewBag.tillBalance = _logic.TellerBalance(id);

            return View();
        }

        //POST: TellerPosting/SellCash
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SellCash(int id)
        {
            var userId = Convert.ToInt32(Session["Id"]);
            var Amount = Convert.ToDecimal(Request.Form["Amount"], CultureInfo.InvariantCulture);

            var Sell = _logic.SellCash(Amount, userId);

            if (Sell != true)
            {
                ViewBag.Message = "You Cannot Sell This Amount";
                ViewBag.Status = false;
                return View();
            }

            ViewBag.Message = "Cash Sold Successfully";
            ViewBag.Status = true;

            return RedirectToAction("ViewAll", "TellerPosting");
        }
    }
}
