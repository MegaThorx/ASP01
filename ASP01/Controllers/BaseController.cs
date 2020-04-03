using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using ASP01.Helpers;
using ASP01.Models;
using Microsoft.AspNet.Identity;

namespace ASP01.Controllers
{
    public class BaseController : Controller
    {
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string cultureName = null;
            
            if (Request.Params["lang"] != null) { 
                cultureName = Request.Params["lang"];
            }
            else if (User.Identity.IsAuthenticated)
            {
                var db = new ApplicationDbContext();
                var userId = User.Identity.GetUserId();

                cultureName = db.Users.Find(userId).Language;
                db.Dispose();
            }
            else
            { 
                cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ? Request.UserLanguages[0] : null;
            }

            cultureName = CultureHelper.GetImplementedCulture(cultureName);
            
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            return base.BeginExecuteCore(callback, state);
        }
    }
}