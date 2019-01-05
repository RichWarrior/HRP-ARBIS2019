using HRP.Arbis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HRP.Arbis.Controllers
{
    public class HandshakeController : BaseController
    {
        // GET: Handshake
        [Authorize]
        public async Task<ActionResult> Index()
        {
            var result = new HandshakeModel();
            var dbResult = await bll.SchoolPosts().ListAsync();
            result.city.Add(new CountryViewModel
            {
                Id = 0,
                Name = "Tümü"
            });
            foreach (var item in dbResult)
            {
                result.view.Add(new HandshakeViewModel
                {
                    SchoolName = item.Schools.Name,
                    Name = item.Name,
                    Category = item.Category1.Name,
                     price = item.Price,
                    Id = item.Id,
                    phone = item.Schools.Phone_Number,
                    email = item.Schools.E_Mail
                });
            }
            var city = await bll.City().ListByAsync();
            foreach (var item in city)
            {
                result.city.Add(new CountryViewModel
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }
            var category = await bll.Category().ListAsync();
            result.cat.Add(new Category {
                Id = "0",
                Name = "Tümü"
            });
            foreach (var item in category)
            {
                result.cat.Add(new Category
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }
            return View(result);
        }
        [Authorize]
        public async Task<ActionResult> Create()
        {
            var result = new HandshakeViewModel();
            var category = await bll.Category().ListAsync();
            foreach (var item in category)
            {
                result.cat.Add(new Category {
                    Id = item.Id,
                     Name = item.Name
                });
            }
            return View(result);
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create(HandshakeViewModel model)
        {
            var user = await UserManager.FindByEmailAsync(User.Identity.Name);
            if (ModelState.IsValid)
            {
                var isSuccess = await bll.SchoolPosts().Add(new DataAccessLayer.Contexts.SchoolPosts
                {
                    School_Id = user.Id,
                    Id = Guid.NewGuid().ToString(),
                     Category = model.category_id,
                     Price = model.price,
                    Name = model.Name
                });
                if (isSuccess)
                    return RedirectToAction("Index", "Handshake");
            }
            ModelState.AddModelError("", "Paylaşım Oluşturulamadı!");
            return View();
        }
        [Authorize]
        public async Task<ActionResult> My()
        {
            var result = new List<HandshakeViewModel>();
            var user = await UserManager.FindByEmailAsync(User.Identity.Name);
            var dbResult = await bll.SchoolPosts().ListByIdAsync(user.Id);
            foreach (var item in dbResult)
            {
                result.Add(new HandshakeViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Category = item.Category1.Name,
                    price = item.Price
                });
            }
            return View(result);
        }
    }
}