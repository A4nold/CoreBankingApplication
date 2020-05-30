using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Profile;
using CBApplicaion.Data.LogicImplementation;
using CBApplication.Core.Models;
using CBApplication.Data.Implementations;

namespace CBApplication.Controllers
{
    public class BankConfigController : Controller
    {
        private readonly BankConfigRepository _contextConfig = new BankConfigRepository(new ApplicationDbContext());


        readonly BankConfig _businessConfig = new BankConfig();
        readonly CurrentConfigLogic _logic = new CurrentConfigLogic();
        readonly EodLogic _eod = new EodLogic();

        public ActionResult Setup()
        {
            

            _businessConfig.FinancialDate = DateTime.Today;
            _businessConfig.IsOpen = false;
            _businessConfig.DayCount = 0;
            _businessConfig.MonthCount = 0;
            _businessConfig.YearCount = 0;

            _contextConfig.Add(_businessConfig);
            _contextConfig.Save(_businessConfig);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Open()
        {

            var getbuisnessConfig = _contextConfig.GetIsOpenFalse();
            var businessConfig = _contextConfig.Get(getbuisnessConfig.Id);

            businessConfig.IsOpen = true;

            _contextConfig.Update(businessConfig);

            TempData["Message"] = "Business Opened Successfully";
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Close()
        {

            var getBusinessConfig = _contextConfig.GetIsOpenTrue();
            var businessConfig = _contextConfig.Get(getBusinessConfig.Id);

            businessConfig.IsOpen = false;

             var checkConfig = _logic.checkConfig();
            
            
            if (checkConfig)
            {
                var runEod = _eod.RunEOD();

                if (runEod == "Success")
                {
                    businessConfig.FinancialDate.AddDays(1);

                    
                    Session["FinancialDate"] = businessConfig.FinancialDate;

                    _contextConfig.Update(businessConfig);

                    TempData["Message"] = "Business Closed Successfully";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Error"] = "Error Occured while running EOD";
                    ViewBag.Status = true;
                }
            }
            else
            {
                TempData["Error"] = "A Configuration is not set";
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
