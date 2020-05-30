using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBApplicaion.Data.LogicImplementation;
using CBApplication.Core.Models;

namespace CBApplication.Controllers
{
    [CheckSession]
    public class CustomerController : Controller
    {
        private readonly CustomerLogic _logic = new CustomerLogic();


        // GET: Customer/Index
        public ActionResult Index()
        {
            var customer = _logic.GetAll();
            return View(customer);
        }
        
        //GET: Customer/EditUser/{id}
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                Customer cust = _logic.Get(id);

                if (cust == null)
                {
                    return HttpNotFound();
                }
                return View(cust);

            }
            return HttpNotFound();
        }

        //POST: Customer/EditUser/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer cust)
        {
            if (cust.id == 0)
            {
                new CustomerLogic().Save(cust);
            }
            else
            {
                var customerInDb = _logic.Get(cust.id);
                customerInDb.FullName = cust.FullName;
                customerInDb.Email = cust.Email;
                customerInDb.Address = cust.Address;
                customerInDb.Gender = cust.Gender;
                customerInDb.PhoneNumber = cust.PhoneNumber;
                _logic.Update(cust);
            }

            return RedirectToAction("Index","Customer");
        }
        
        //GET: Customer/New
        [CheckSession]
        public ActionResult New()
        {
            return View();
        }

        //POST: Save
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer cust)
        {
            //Generating random customerID
            cust.customerId = _logic.GenerateCustomerId();

            //saving values into db
            _logic.Save(cust);
            return RedirectToAction("Index", "Customer");
        }

        // GET: Customer/Details
        public ActionResult Details(int? id)
        {
            var cust = _logic.Get(id);
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