
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;
using CBApplicaion.Data.LogicImplementation;
using CBApplication.Core.Models;
using CBApplication.Core.ViewModels;
using CBApplication.Data.Implementations;

namespace CBApplication.Controllers
{
    [RestrictNonAdmins]
    public class GlCategoryManagementController : Controller
    {
        //private readonly UserLogic _context = new UserLogic();
        private static readonly GlCategoryRepository _category = new GlCategoryRepository(new ApplicationDbContext());
        private static readonly GlAccountRepository _glAccount = new GlAccountRepository(new ApplicationDbContext());

        GlCategoryLogic Logic = new GlCategoryLogic();

        //Initializing Object for viewModel
        public GlCategoryManagementViewModels ViewModel = new GlCategoryManagementViewModels
        {
            MainCategory = _category.GetAllMainAccountCategories()
        };


        //GET: GlCategory/Create
        public ActionResult Create()
        {
            return View(ViewModel);
        }

        //POST: GlCategory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GlCategoryManagement category)
        {

            category.Name = Request.Form["catName"];
            category.Description = Request.Form["catDescription"];
            category.MainAccountCategoryId = Request.Form["catCategory"];
            

            try
            {
                if (!Logic.IsGlNameExist(Request.Form["catName"]))
                {
                    //Adding information into database

                    //db.Entry(category).State = EntityState.Modified;
                    _category.Add(category);

                    //Saving information into database
                    _category.Save(category);

                    return RedirectToAction("Index", "GlCategoryManagement");
                }

                ViewBag.Message = "Gl Category Name Already Exist";
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

            return View(ViewModel);
        }

        //GET: GlCategory/Index
        public ActionResult Index()
        {
            var categoriesList = _category.GetAll();
            return View(categoriesList);
        }

        //GET: GlCategory/Details/{id}
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            GlCategoryManagement category = _category.Get(id);

            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);

        }

        //GET: GlCategory/Edit/{id}
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                GlCategoryManagement category = _category.Get(id);

                GlCategoryManagementViewModels viewModel = new GlCategoryManagementViewModels
                {
                    MainCategory = _category.GetAllMainAccountCategories(),
                    GlCategoryManagements = category
                };

                if (category != null)
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
        public ActionResult Edit(GlCategoryManagement category)
        {
            if (category.Id == 0)
            {
                _category.Save(category);
            }
            else
            {
                var categoryInDb = _category.Get(category.Id);
                var getGlCat= _glAccount.GetCatName(categoryInDb.Name);
                var glAccount = _glAccount.Get(getGlCat.Id);
                categoryInDb.Name = Request.Form["catName"];
                categoryInDb.Description = Request.Form["catDescription"];
                glAccount.CategoryManagementId = categoryInDb.Name;

                _category.Update(category);
                _glAccount.Update(glAccount);
            }

            return RedirectToAction("Index", "GlCategoryManagement");
        }
    }
}
