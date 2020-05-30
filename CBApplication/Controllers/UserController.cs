using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using CBApplicaion.Data.LogicImplementation;
using CBApplication.Core.Models;
using CBApplication.Core.ViewModels;
using CBApplication.Data.Implementations;
using Microsoft.Ajax.Utilities;

namespace CBApplication.Controllers
{
    
    public class UserController : Controller
    {
        private readonly UserLogic _context = new UserLogic();
        private readonly BankConfigRepository _bankContext = new BankConfigRepository(new ApplicationDbContext());
        private static readonly BranchRepository BranchContext = new BranchRepository(new ApplicationDbContext());
        //private ApplicationDbContext db= new ApplicationDbContext();
        

        //Initializing Object for viewModel
        public NewUserViewModel viewModel = new NewUserViewModel
        {
            Branches = BranchContext.GetAll()
            
        };


        // GET: User
        [RestrictNonAdmins]
        public ActionResult Register()
        {
            
            return View(viewModel);
        }

        //POST: User/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [RestrictNonAdmins]
        public ActionResult Register([Bind(Exclude = "Password")] User user)
        {
            bool status = false;
            string message = "";

            //Model Validation
            if (ModelState.IsValid)
            {
                //Email already exist
                var itExist = _context.IsEmailExist(user.Email);

                if (itExist)
                {
                    ModelState.AddModelError("EmailExist", "Email Already Exist");
                    return View(viewModel);
                }
                //Username already exist
                var userExist = _context.IsUserNameExist(user.Username);

                if (userExist)
                {
                    ModelState.AddModelError("UserExist", "Username Already Exist");
                    return View(viewModel);
                }

                //Generate Random Password
                //user.Password = new UserLogic().CreateRandpword();

                var realpassword = _context.CreateRandpword();

                var hashPassword = Crypto.Hash(realpassword);
                user.Password = hashPassword;

                //Sending Username and Password to New User
                var sendmail = _context.SendingEmail(user.Email, realpassword, user.Username);

                if (sendmail != true)
                {
                    ViewBag.Message = "Email Failed to send, Registration Failed";
                    return View(viewModel);
                }

                //Saving data into database
                _context.Save(user);                


                message = "Registration Success, Visit your email for login details";
                status = true;

            }
            else
            {
                message = "Error occured during registration";
            }



            ViewBag.Message = message;
            ViewBag.Status = status;
            return View(viewModel);
        }

        [RestrictNonAdmins]
        public ActionResult Index()
        {
            var user = _context.GetAll();
            return View(user);
        }

        // GET: User/Login

        public ActionResult Login(int? id)
        {
            return View();
        }

        // POST: User/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Validate(User user)
        {
            if (user == null)
            {
                return View("Login");
            }


            var cheUser = _context.GetByUsername(user.Username);


            //Getting and comparing Login Email to registration Email
            var password = Crypto.Hash(user.Password).Trim();

            var checkDetails = new UserLogic().checkDetails(user.Username, password);
            var bank = _bankContext.Get(1);

            if (checkDetails)
            {
                Session["ID"] = cheUser.Id;
                Session["Password"] = cheUser.Password;
                Session["Name"] = cheUser.Username;
                Session["Role"] = cheUser.Role;
                Session["FinancialDate"] = bank.FinancialDate.ToString("D");

                if (cheUser.PassChanged == true)
                {
                    return RedirectToAction("Index", "Home");
                }

                if (cheUser.PassChanged == false)
                {
                    return RedirectToAction("ChangePassword", "User");
                }
            }

            var msg = "Incorrect Username or Password";
            ViewBag.msg = msg;

            return View("Login");
        }

        //GET: User/EditUser/{id}
        [RestrictNonAdmins]
        public ActionResult Edit(int? id)
        {
            if (id == 0)
            {
                return HttpNotFound();
            }
            User users = _context.Get(id);
            if (users == null)
            {
                return HttpNotFound();
            }

            NewUserViewModel viewModels = new NewUserViewModel
            {
                Branches = BranchContext.GetAll(),
                user = users
            };

            return View(viewModels);
        }

        //POST: User/EditUser/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RestrictNonAdmins]
        public ActionResult Edit(User user)
        {
            if (user.Id == 0)
            {
                new UserLogic().Save(user);
            }
            else
            {
                var customerInDb = _context.Get(user.Id);

                customerInDb.FullName = user.FullName;
                customerInDb.Email = user.Email;

                customerInDb.Branch_Name = user.Branch_Name;
                customerInDb.PhoneNumber = user.PhoneNumber;
                customerInDb.Role = user.Role;

                _context.Update(user);
            }

            return RedirectToAction("Index", "User");
        }

        // GET: User/Details
        [RestrictNonAdmins]
        public ActionResult Details(int? id)
        {
            var user = _context.Get(id);
            if (id == null)
            {
                return HttpNotFound();
            }

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        //Logout
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            return RedirectToAction("Login", "User");
        }

        //GET: User/ChangePassword
        public ActionResult ChangePassword()
        {
            if (Session["ID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        //POST: User/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(User user)
        {
            var val = Request.Form["session"];
            var sessionID = Convert.ToInt32(val);

            string message = "";

            if (sessionID == 0)
            {
                HttpNotFound();
            }
            else
            {
                var userInDb = _context.UserInDb(sessionID);

                var newpass = Request.Form["newPass"];

                var newHashPass = Crypto.Hash(newpass);

                var oldHashPass = Crypto.Hash(user.Password).Trim();

                if (userInDb.Password == oldHashPass)
                {
                    
                    if (newpass == Request.Form["conPass"])
                    {
                        try
                        {
                            userInDb.Password = newHashPass;
                            userInDb.PassChanged = true;

                            //db.Entry(userInDb).State = EntityState.Modified;
                            _context.Update(userInDb);

                            TempData["Message"] = "Password Change Successful";
                            return RedirectToAction("Index", "Home");
                        }
                        catch (DbEntityValidationException e)
                        {
                            var errmsg = e.EntityValidationErrors
                                .SelectMany(x => x.ValidationErrors)
                                .Select(x => x.ErrorMessage);

                            var fullerrmsg = string.Join(";", errmsg);
                            var execeptionmsg = string.Concat(e.Message, "The validation errors are: ", fullerrmsg);

                            throw new DbEntityValidationException(execeptionmsg,e.EntityValidationErrors);
                        }
                        
                    }
                    else
                    {
                        //message = "Passwords do not match";
                        ModelState.AddModelError("PassMatchFail", "Password Does Not Match");
                        return View(user);
                    }
                }
                else
                {
                    //message = "wrong password";
                    ModelState.AddModelError("PassIncorrect", "Old Password Incorrect");
                    return View(user);
                }
            }

            return View(user);
        }

        //Welcome page Action
        public ActionResult Welcome()
        {
            if (new UserLogic().isRoleCheck(Session["Role"]) == true)
            {
                return View();
            }

            return HttpNotFound();
        }
    }
}