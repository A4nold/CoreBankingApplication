using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
    public class GlAccountManagementController : Controller
    {
        private readonly GlAccountLogic _logic = new GlAccountLogic();
        private readonly GlAccountRepository _accountcontext = new GlAccountRepository(new ApplicationDbContext());
        private readonly GlCategoryRepository _categoryContext = new GlCategoryRepository(new ApplicationDbContext());
        private readonly ApplicationDbContext _db = new ApplicationDbContext();


       

       //GET: GlAccount/Create
       public ActionResult Create()
       {
            //Initializing Object for viewModel
            GlAccountManagementViewModels viewModel = new GlAccountManagementViewModels
           {
               Categories = _accountcontext.GetAllGlCategories(),
               Branches = _accountcontext.GetBranches()
           };
           return View(viewModel);
       }

       //GET: GlAccount/Index
       public ActionResult Index()
       {
           var accountList = _accountcontext.GetAll();
           return View(accountList);
       }


        //POST: GlAccount/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GlAccountManagement account)
       {
           //Initializing Object for viewModel
           GlAccountManagementViewModels viewModel = new GlAccountManagementViewModels
           {
               Categories = _accountcontext.GetAllGlCategories(),
               Branches = _accountcontext.GetBranches()
           };

           

           account.Name = Request.Form["AcctName"];
           var itExists = _logic.IsNameExist(Request.Form["AcctName"].ToLower());

           if (itExists)
           {
               ModelState.AddModelError("NameExist", "Name Already Exist");
               return View(viewModel);
           }

           account.CategoryManagementId = Request.Form["AcctCategory"];
           account.BranchId = Request.Form["Branches"];
           account.AccountBalance = 0;
           account.AccountAssigned = false;
           var catMgtId = Convert.ToInt32(account.CategoryManagementId) ;
           var findGlCat = _db.GlCategory.FirstOrDefault(f => f.Id == catMgtId);

           account.MainAccountCategoryId = findGlCat.MainAccountCategoryId;
           if (account.MainAccountCategoryId == "Asset")
           {
               account.AccountAssigned = false;
           }
           else
           {
               account.AccountAssigned = true;

           }

           var accountCategory =  Convert.ToInt32(Request.Form["AcctCategory"]);

           var mainCat = _categoryContext.GetMainCat(accountCategory);
           account.MainAccountCategoryId = mainCat;

            account.AccCode = _logic.GenerateGlAccountCode(mainCat);
            account.CategoryManagementId = _categoryContext.GetCategoryName(Convert.ToInt32(Request.Form["AcctCategory"]));


            try
           {
               //Adding information into database

               _accountcontext.Add(account);

               //Saving information into database
               _accountcontext.Save(account);

               return RedirectToAction("Index", "GlAccountManagement");

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
           //return View(viewModel);
       }

       //GET: GlAccount/Edit
       public ActionResult Edit(int? id)
       {
           if (id != null)
           {
               GlAccountManagement account = _accountcontext.Get(id);
               GlAccountManagementViewModels viewModel = new GlAccountManagementViewModels
               {
                   Categories = _accountcontext.GetAllGlCategories(),
                   Branches = _accountcontext.GetBranches(),
                   AccountManagement = account
               };

               if (account != null)
               {
                   return View(viewModel);
               }

               return HttpNotFound();

           }
           return HttpNotFound();
        }

       //POST: GlCategory/Edit/{id}
       [HttpPost]
       [ValidateAntiForgeryToken]
       public ActionResult Edit([Bind(Exclude = "MainAccountCategory,MainAccountCategoryId")]
           GlAccountManagement account)
       {
           //Initializing Object for viewModel
           GlAccountManagementViewModels viewModel = new GlAccountManagementViewModels
           {
               Categories = _accountcontext.GetAllGlCategories(),
               Branches = _accountcontext.GetBranches()
           };

            if (account.Id == 0)
           {
               _accountcontext.Save(account);
           }
           else
           {
               var categoryInDb = _accountcontext.Get(account.Id);
               var itExists = _logic.IsNameExist(Request.Form["glName"].ToLower());

               if (itExists)
               {
                   ModelState.AddModelError("NameExist", "Name already exist");
                   return View(viewModel);
               }

               categoryInDb.Name = Request.Form["glName"];
               categoryInDb.BranchId = Request.Form["Branches"];

                _accountcontext.Update(account);
           }

           return RedirectToAction("Index", "GlAccountManagement");
       }
    }
}
