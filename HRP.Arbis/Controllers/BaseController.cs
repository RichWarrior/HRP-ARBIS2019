using HRP.Arbis.DataAccessLayer;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Web.Mvc;

namespace HRP.Arbis.Controllers
{
    public class BaseController : Controller
    {
        public ApplicationSignInManager _signInManager;
        public ApplicationUserManager _userManager;
        public RoleManager<IdentityRole> _roleManager;
        public BLL bll;


        public BaseController()
        {
            bll = new BLL();
            _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
        }

        public BaseController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;            
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
    }
}