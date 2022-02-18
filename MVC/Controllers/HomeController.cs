using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MVC.Models;
using MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationContext _db;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, UserManager<User> userManager, ApplicationContext db)
        {
            _logger = logger;
            _userManager = userManager;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult VolReq()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> VolReq(VolRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                VolRequest volReq = new VolRequest { Age = model.Age, Sex = model.Sex, LivArea = model.LivArea, Phone = model.Phone, UserId = _userManager.GetUserId(User)};
                // добавляем заявку в бд
                _db.VolRequests.Add(volReq);
                await _db.SaveChangesAsync();      
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult SearchReq()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SearchReq(SearchRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                SearchRequest searchReq = new SearchRequest {FullName = model.FullName, Age = model.Age, Sex = model.Sex, MissArea = model.MissArea, MissTime = model.MissTime, AddInf = model.AddInf, UserId = _userManager.GetUserId(User) };
                // добавляем заявку в бд
                _db.SearchRequests.Add(searchReq);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}
