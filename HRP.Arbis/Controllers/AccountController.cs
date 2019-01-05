using HRP.Arbis.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HRP.Arbis.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.email = model.email;
                var user = await UserManager.FindAsync(model.email, model.password);
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, true, true);
                    return RedirectToAction("Index", "Home");
                }               
            }
            ModelState.AddModelError("", "Kullanıcı Adı Şifre Yanlış!");
            return View();
        }

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> ForgotPassword(ForgotViewModel model)
        {
            //Mail Gönder
            var user = await UserManager.FindByEmailAsync(model.email);
            if (user != null)
            {
                var password = Guid.NewGuid().ToString();
                var passwordHash = "";
                for (int i = 0; i < 7; i++)
                {
                    passwordHash += password[i].ToString();
                }
                user.PasswordHash = UserManager.PasswordHasher.HashPassword(passwordHash);
                var result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    var b = await bll.Action().Add(new DataAccessLayer.Contexts.Actions {
                        email = model.email,
                        Message = String.Format("HRP ARBIS SIFRENIZ {0}",passwordHash),
                        Is_Completed = 0
                    });
                    return RedirectToAction("Login", "Account");
                }
            }
            ModelState.AddModelError("", "Şifreniz Değiştirilemedi!");
            return View();
        }
        [Authorize]
        public async Task<ActionResult> EditAccount()
        {
            try
            {
                var result = new SchoolProfileViewModel();
                var city = await bll.City().ListByAsync();
                foreach (var item in city)
                {
                    result.City.Add(new CountryViewModel
                    {
                        Id = item.Id,
                        Name = item.Name
                    });
                    foreach (var ds in item.District)
                    {
                        result.District.Add(new DistrictViewModel
                        {
                            Id = ds.Id,
                            Name = ds.Name
                        });
                    }

                }
                var type = await bll.SType().ListAsync();
                foreach (var item in type)
                {
                    result.type.Add(new SchoolTypeViewModel
                    {
                        Id = item.Id,
                        Name = item.Name
                    });
                }
                var user = await UserManager.FindByEmailAsync(User.Identity.Name);
                var dbResult = await bll.School().FindByIdAsync(user.Id);
                result.Id = user.Id;
                result.Name = dbResult.Name;
                result.phoneNumber = dbResult.Phone_Number;
                result.E_Mail = User.Identity.Name;
                result.address = dbResult.Address;
                result.CityId = dbResult.City_Id;
                result.District_Id = dbResult.District_Id;
                result.type_id = dbResult.SchoolType;
                return View(result);
            }
            catch (Exception)
            {
            }
            return HttpNotFound();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> EditAccount(SchoolProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(User.Identity.Name);
                if (model.Password != null)
                {
                    user.PasswordHash = UserManager.PasswordHasher.HashPassword(model.Password);
                    var result = await UserManager.UpdateAsync(user);                    
                }
                var updateSuccess = await bll.School().Update(user.Id, new DataAccessLayer.Contexts.Schools
                {
                    City_Id = model.CityId,
                    District_Id = model.District_Id,
                    Phone_Number = model.phoneNumber,
                    Address = model.address,
                     SchoolType = model.type_id
                });
                if (updateSuccess)
                    return RedirectToAction("Index", "Home");

            }
            ModelState.AddModelError("", "Bilgileriniz Güncellenemedi");
            return View(model);
        }
        [Authorize]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}