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
                VolRequest volReq = new VolRequest { Age = model.Age, Sex = model.Sex, LivArea = model.LivArea, Phone = model.Phone, Status = "Ожидание", UserId = _userManager.GetUserId(User)};
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
                SearchRequest searchReq = new SearchRequest {FullName = model.FullName, Age = model.Age, Sex = model.Sex, MissArea = model.MissArea, MissTime = model.MissTime, AddInf = model.AddInf, Status = "Ожидание", UserId = _userManager.GetUserId(User) };
                // добавляем заявку в бд
                _db.SearchRequests.Add(searchReq);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        public IActionResult CheckOperations()
        {
            return View(_db.Operations.Include(o => o.SearchRequest).ToList());
        }

        public async Task<IActionResult> OperationDetails(int id)
        {
            Operation operation = await _db.Operations.Include(o => o.SearchRequest).Include(o => o.Users).FirstOrDefaultAsync(o => o.Id == id);
            if (operation == null)
            {
                return NotFound();
            }
            ViewBag.User = await _userManager.GetUserAsync(User);
            return View(operation);
        }

        public async Task<IActionResult> JoinOperation(int id)
        {
            User user = await _userManager.GetUserAsync(User);
            Operation operation = await _db.Operations.FirstOrDefaultAsync(o => o.Id == id);
            if (user == null || operation == null)
            {
                return NotFound();
            }
            operation.Users.Add(user);
            await _db.SaveChangesAsync();
            return RedirectToAction("OperationDetails", "Home", new { id = id });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}
