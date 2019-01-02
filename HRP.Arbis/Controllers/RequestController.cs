using HRP.Arbis.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HRP.Arbis.Controllers
{
    /// <summary>
    /// Json İşlemleri
    /// </summary>
    public class RequestController : BaseController
    {
        [Authorize]
        public async Task<JsonResult> Menu()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await UserManager.FindByEmailAsync(User.Identity.Name);
                var roleToUser = await UserManager.GetRolesAsync(user.Id);
                var roleId = await _roleManager.FindByNameAsync(roleToUser.FirstOrDefault());
                var dbResult = await bll.Menu().ListByRoleAsync(roleId.Id);
                var result = new List<MenuViewModel>();
                foreach (var item in dbResult)
                {
                    result.Add(new MenuViewModel
                    {
                        Id = item.Id,
                        Action = item.Action,
                        Controller = item.Controller,
                        Icon = item.Icon,
                        Name = item.Name,
                        Nested = item.Nested,
                        Rank = item.Rank
                    });
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Sistem Yöneticisi")]
        public async Task<JsonResult> DeleteSchool(string id)
        {
            var result = false;
            var schoolDelete = await bll.School().Delete(id);
            if (schoolDelete)
            {
                var user = await UserManager.FindByIdAsync(id);
                var identity = await UserManager.DeleteAsync(user);
                if (identity.Succeeded)
                    result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Sistem Yöneticisi")]
        public async Task<JsonResult> DeleteParameter(string id)
        {
            var result = await bll.ServerInfo().Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [Authorize]
        public async Task<JsonResult> CityDistricts(int id)
        {
            var result = new List<DistrictViewModel>();
            var dbResult = await bll.Districts().ListByCityIdAsync(id);
            foreach (var item in dbResult)
            {
                result.Add(new DistrictViewModel
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [Authorize]
        public async Task<JsonResult> SchoolByDistrict(int id)
        {
            var result = new List<SchoolViewModel>();
            var dbResult = await bll.School().ListByDistrictAsync(id);
            foreach (var item in dbResult)
            {
                result.Add(new SchoolViewModel
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }
            return Json(result,JsonRequestBehavior.AllowGet);
        }
        [Authorize]
        public async Task<JsonResult> SchoolPostsByDistrictId(int id)
        {
            var result = new List<HandshakeViewModel>();
            var dbResult = await bll.SchoolPosts().ListByDistrictIdAsync(id);
            foreach (var item in dbResult)
            {
                result.Add(new HandshakeViewModel {
                    Id = item.Id,
                    Description = item.Description,
                    Name = item.Name,
                    SchoolName = item.Schools.Name,
                    phone = item.Schools.Phone_Number,
                    email = item.Schools.E_Mail
                });
            }
            return Json(result,JsonRequestBehavior.AllowGet);
        }
        [Authorize]
        public async Task<JsonResult> DeletePosts(string id)
        {
            var result = false;
            result = await bll.SchoolPosts().Delete(id);
            return Json(result,JsonRequestBehavior.AllowGet);
        }
    }
}