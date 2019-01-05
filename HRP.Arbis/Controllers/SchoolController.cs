using HRP.Arbis.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HRP.Arbis.Controllers
{
    public class SchoolController : BaseController
    {
        // GET: School
        [Authorize(Roles = "Sistem Yöneticisi")]
        public async Task<ActionResult> Index()
        {
            var dbResult = await bll.School().ListByAsync();
            var result = new List<SchoolViewModel>();
            foreach (var item in dbResult)
            {
                result.Add(new SchoolViewModel
                {
                    Id = item.Id,
                    E_Mail = item.E_Mail,
                    Name = item.Name
                });
            }
            return View(result);
        }
        [Authorize(Roles = "Sistem Yöneticisi")]
        public async Task<ActionResult> Create()
        {
            var result = new SchoolViewModel();
            var dbResult = await bll.SType().ListAsync();
            foreach (var item in dbResult)
            {
                result.type.Add(new SchoolTypeViewModel
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }
            return View(result);
        }
        [Authorize(Roles = "Sistem Yöneticisi")]
        [HttpPost]
        public async Task<ActionResult> Create(SchoolCreateModel model)
        {
            if (ModelState.IsValid)
            {
                //Mail Gönderilecek
                model.Id = Guid.NewGuid().ToString();
                model.E_Mail += "@meb.k12.tr";
                var password = Guid.NewGuid().ToString();
                var passwordHash = "";
                for (int i = 0; i < 6; i++)
                {
                    passwordHash += password[i].ToString();
                }
                var user = new ApplicationUser();
                user.Id = model.Id;
                user.Email = model.E_Mail;
                user.UserName = user.Email;
                var createUser = await UserManager.CreateAsync(user, passwordHash);
                if (createUser.Succeeded)
                {
                    var roleToAdd = await UserManager.AddToRoleAsync(user.Id, "Normal Kullanıcı");
                    if (roleToAdd.Succeeded)
                    {
                        var isSuccess = await bll.School().Add(new DataAccessLayer.Contexts.Schools
                        {
                            Id = model.Id,
                            E_Mail = model.E_Mail,
                            Name = model.Name,
                            SchoolType = model.type_id
                        });
                        if (isSuccess)
                        {
                            ///DİKKAT

                            var b = await bll.Action().Add(new DataAccessLayer.Contexts.Actions
                            {
                                email = model.E_Mail,
                                Message = String.Format("HRP ARBIS SIFRENIZ {0}", passwordHash),
                                Is_Completed = 0
                            });
                            return RedirectToAction("Index", "School");
                        }
                    }
                }
                ModelState.AddModelError("", "Okul Oluşturulamadı");
            }
            return View();
        }
    }
}