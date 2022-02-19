﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Models;
using MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationContext _db;

        public AdminController(UserManager<User> userManager, ApplicationContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        public IActionResult AdminPanel()
        {
            return View(_userManager.Users.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Name = model.Name, SurName = model.SurName, Email = model.Email, UserName = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "user");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        public IActionResult Details()
        {
            return NotFound();
        }

        public async Task<IActionResult> Edit(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            EditUserViewModel model = new EditUserViewModel { Id = user.Id, Email = user.Email, Name = user.Name, SurName = user.SurName };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.Email;
                    user.Name = model.Name;
                    user.SurName = model.SurName;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ManageVolReq()
        {
            return View(_db.VolRequests.Include(v => v.User).ToList());
        }

        public IActionResult ManageSearchReq()
        {
            return View(_db.SearchRequests.Include(s => s.User).ToList());
        }

        public async Task<ActionResult> ResponseVolReq(int id)
        {
            VolRequest volReq = await _db.VolRequests.Include(v => v.User).FirstOrDefaultAsync(v => v.Id == id);
            if (volReq == null)
            {
                return NotFound();
            }
            return View(volReq);
        }

        [HttpGet]
        public async Task<ActionResult> ResponseSearchReq(int id)
        {
            SearchRequest searchReq = await _db.SearchRequests.FirstOrDefaultAsync(s => s.Id == id);
            if (searchReq == null)
            {
                return NotFound();
            }
            return View(searchReq);
        }

        [HttpGet]
        public async Task<ActionResult> AcceptVolReq(int id)
        {
            VolRequest volReq = await _db.VolRequests.Include(v => v.User).FirstOrDefaultAsync(v => v.Id == id);
            await _userManager.AddToRoleAsync(volReq.User, "volunteer");
            _db.VolRequests.Remove(volReq);
            await _db.SaveChangesAsync();
            return RedirectToAction("ManageVolReq");
        }

        [HttpGet]
        public async Task<ActionResult> DeclineVolReq(int id)
        {
            VolRequest volReq = await _db.VolRequests.FirstOrDefaultAsync(v => v.Id == id);
            _db.VolRequests.Remove(volReq);
            await _db.SaveChangesAsync();
            return RedirectToAction("ManageVolReq");
        }
    }
}
