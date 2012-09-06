using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using SportsStore.WebUI.Models;
using SportsStore.Domain.Abstract;

namespace SportsStore.Controllers
{
    public class AccountController : Controller
    {
        IAutProvider authProvider;

        public AccountController(IAutProvider auth)
        {
            this.authProvider = auth;
        }

        public ViewResult LogOn()
        {
            return View();
        }

       

        [HttpPost]
        public ActionResult LogOn(LogOnViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if ((authProvider.Authenticate(model.UserName, model.Password)))
                {
                    return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect userName");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
    }
}
