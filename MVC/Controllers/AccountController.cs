using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using MVC.ViewModels;
using MVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System;

namespace MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationContext _db;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Name = model.Name, SurName = model.SurName, Email = model.Email, UserName = model.Email };
                // добавляем пользователя в бд
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // установка роли
                    await _userManager.AddToRoleAsync(user, "user");
                    // установка куки
                    await _signInManager.SignInAsync(user, false);
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

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {                  
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> EditProfile()
        {
            User user = await _userManager.GetUserAsync(User);           
            if (user == null)
            {
                return NotFound();
            }
            if (User.IsInRole("volunteer"))
            {
                VolRequest volReq = await _db.VolRequests.FirstOrDefaultAsync(v => v.UserId == user.Id);
                VolunteerProfileViewModel model = new VolunteerProfileViewModel { Email = user.Email, Name = user.Name, SurName = user.SurName, Phone = volReq.Phone, Age = volReq.Age,
                                                                                  Sex = volReq.Sex, LivArea = volReq.LivArea };
                return View("EditVolunteerProfile", model);
            }
            else
            {
                UserProfileViewModel model = new UserProfileViewModel { Email = user.Email, Name = user.Name, SurName = user.SurName };
                return View(model);
            }           
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditProfile(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.Email;
                    user.Name = model.Name;
                    user.SurName = model.SurName;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return View(model);
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

        public IActionResult ChangePassword()
        {
            return View(new ChangePasswordViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
                if (user != null)
                {
                    IdentityResult result =
                        await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
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
                else
                {
                    ModelState.AddModelError(string.Empty, "Пользователь не найден");
                }
            }
            return View(model);
        }

        [Authorize(Roles = "admin,volunteer")]
        public async Task<IActionResult> GetChats()
        {
            User user = await _userManager.GetUserAsync(User);
            return View(_db.Chats.Include(c => c.SearchRequest).Where(c => c.Users.Contains(user)));
        }

        [Authorize(Roles = "admin,volunteer")]
        public async Task<IActionResult> OpenChat(int id)
        {
            Chat chat = await _db.Chats.Include(c => c.Messages).FirstOrDefaultAsync(c => c.Id == id);
            return View(chat);
        }

        [Authorize(Roles = "admin,volunteer")]
        public async Task<IActionResult> CreateMessage(int id, string text)
        {
            Chat chat = await _db.Chats.FirstOrDefaultAsync(c => c.Id == id);          
            User user = await _userManager.GetUserAsync(User);
            Message message = new Message() { ChatId = id, Text = text, Timestamp = DateTime.Now, UserName = $"{user.Name} {user.SurName}", UserId = user.Id };
            chat.Messages.Add(message);
            _db.Messages.Add(message);
            await _db.SaveChangesAsync();
            return RedirectToAction("OpenChat", "Account", message.Chat);
        }      
    }
}