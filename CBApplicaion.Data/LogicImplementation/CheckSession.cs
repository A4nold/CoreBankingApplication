﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CBApplicaion.Data.LogicImplementation
{
    public class CheckSession : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var mySession = HttpContext.Current.Session;

            var sessionToString = (string)mySession["role"];

            if (string.IsNullOrEmpty(sessionToString))
            {
                filterContext.Result = new RedirectResult(string.Format("~/User/Logout/"));
            }
        }
    }
}
