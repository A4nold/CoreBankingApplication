using System;
using System.Web.Mvc;
using CBApplicaion.Data.LogicImplementation;
using CBApplication.Core.Models;
using CBApplication.Core.ViewModels;
using CBApplication.Data.Implementations;

namespace CBApplication.Controllers
{
    [RestrictNonAdmins]
    [CheckSession]
    public class TellerManagementController : Controller
    {
        private static readonly GlAccountRepository Accountcontext = new GlAccountRepository(new ApplicationDbContext());
        private static readonly UserRepository _user = new UserRepository(new ApplicationDbContext());
        private static readonly TellerRepository TellerContext = new TellerRepository(new ApplicationDbContext());
        //private ApplicationDbContext db = new ApplicationDbContext();

        public TellerManagementViewModels Viewmodel = new TellerManagementViewModels
        {
            Users = _user.GetAllUnassignedUsers(),
            Category = Accountcontext.GetAllUnassignedUsers()
        };

        //GET: TellerManagement/Create
        public ActionResult Create()
        {
            return View(Viewmodel);
        }

        //GET: TellerManagement/Index
        public ActionResult Index()
        {
            var tillList = TellerContext.GetAllUsersAndAccount();
            return View(tillList);

        }

        //POST: TellerManagement/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TellerManagement teller)
        {
            teller.userId = Convert.ToInt32(Request.Form["Users"]);
            teller.AccountManagementId = Convert.ToInt32(Request.Form["Account"]);
            teller.Date = DateTime.Now.ToString();

            var user = new User();
            var glAccount = new GlAccountManagement();

            var id = Convert.ToInt32(teller.userId);
            var userInDb = _user.Get(id);
            var glAccountInDb = Accountcontext.Get(teller.AccountManagementId);

            
            
                //Updating userassigned
                userInDb.UserAssigned = true;
                _user.Update(user);

                //Updating AccountAssigned
                glAccountInDb.AccountAssigned = true;
                Accountcontext.Update(glAccount);
                
                //Adding into database
                TellerContext.Add(teller);

                //saving for teller
                TellerContext.Save(teller);

                return RedirectToAction("Index", "TellerManagement");

            

            //return View(viewmodel);
        }
    }
}
