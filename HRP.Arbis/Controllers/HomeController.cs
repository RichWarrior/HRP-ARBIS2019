using HRP.Arbis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HRP.Arbis.Controllers
{
    public class HomeController : BaseController
    {
        [Authorize]
        public async Task<ActionResult> Index()
        {
            var user = await UserManager.FindByEmailAsync(User.Identity.Name);
            var roles =await UserManager.GetRolesAsync(user.Id);
            if (roles.FirstOrDefault() == "Normal Kullanıcı")
                return RedirectToAction("Index","Handshake");
            else
                return RedirectToAction("Index","Server");
        }     
        public ActionResult Error()
        {
            return View();
        }
    }
}