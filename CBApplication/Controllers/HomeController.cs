using System;
using System.Web.Mvc;
using CBApplicaion.Data.LogicImplementation;

namespace CBApplication.Controllers
{
    [CheckSession]
    public class HomeController : Controller
    {
        private TellerPostingLogic _logic = new TellerPostingLogic();
        private CustomerAccountLogic _customerAccount = new CustomerAccountLogic();
        private CustomerLogic _customer = new CustomerLogic();
        private GlAccountLogic _gl = new GlAccountLogic();
        private LoanLogic _loan = new LoanLogic();
        private UserLogic _user = new UserLogic();

        [HttpGet]
        public ActionResult Index()
        {
            TempData["tillAccounts"] = _logic.NumberOfTillAccounts();
            TempData["customerAccounts"] = _customerAccount.NumberOfCustomerAccounts();
            TempData["customers"] = _customer.NumberOfCustomers();
            TempData["GlAccounts"] = _gl.NumberOfGlAccounts();
            TempData["loanAccounts"] = _loan.NumberOfLoanAccounts();
            TempData["tellers"] = _user.NumberOfTellers();
            TempData["admin"] = _user.NumberOfAdmins();

            return View();
        }

        [HttpGet]
        public ActionResult AnotherLink()
        {
            return View("Index");
        }
    }
}
