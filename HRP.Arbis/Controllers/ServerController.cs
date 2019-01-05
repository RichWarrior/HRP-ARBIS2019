using HRP.Arbis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HRP.Arbis.Controllers
{
    public class ServerController : BaseController
    {
        // GET: Server
        [Authorize(Roles = "Sistem Yöneticisi")]
        public async Task<ActionResult> Index()
        {
            var result = new List<ServerParamaterViewModel>();
            foreach (var item in await bll.ServerInfo().ListByAsync())
            {
                result.Add(new ServerParamaterViewModel
                {
                    Id = item.Id,
                    key_str = item.key_str,
                    value_str = item.value_str
                });
            }
            return View(result);
        }
        [Authorize(Roles = "Sistem Yöneticisi")]
        public async Task<ActionResult> EditParameter(string id)
        {
            var dbResult = await bll.ServerInfo().FindByIdAsync(id);
            if (dbResult != null)
            {
                var result = new ServerParamaterViewModel();
                result.Id = id;
                result.key_str = dbResult.key_str;
                result.value_str = dbResult.value_str;
                return View(result);
            }
            else
                return HttpNotFound();
        }
        [Authorize(Roles = "Sistem Yöneticisi")]
        [HttpPost]
        public async Task<ActionResult> EditParameter(string id, ServerParamaterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var isSuccess = await bll.ServerInfo().Update(id, new DataAccessLayer.Contexts.ServerInfo
                {
                    key_str = model.key_str,
                    value_str = model.value_str
                });
                if (isSuccess)
                    return RedirectToAction("Index", "Server");
            }
            ModelState.AddModelError("", "Parametre Güncellenemedi!");
            return View();
        }
        [Authorize(Roles = "Sistem Yöneticisi")]
        public ActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Sistem Yöneticisi")]
        [HttpPost]
        public async Task<ActionResult> Create(ServerParamaterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var isSuccess = await bll.ServerInfo().Add(new DataAccessLayer.Contexts.ServerInfo
                {
                    Id = Guid.NewGuid().ToString(),
                    key_str = model.key_str,
                    value_str = model.value_str
                });
                if (isSuccess)
                    return RedirectToAction("Index", "Server");
            }
            ModelState.AddModelError("", "Parametre Oluşturulamadı!");
            return View();
        }
    }
}