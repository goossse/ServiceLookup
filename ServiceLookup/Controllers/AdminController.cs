using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceLookup.BL.DTO;
using ServiceLookup.BL.Services.Interfaces;
using ServiceLookup.DAL.Entity;

namespace ServiceLookup.Controllers
{
    [Authorize(Roles ="admin")]
    public class AdminController : Controller
    {
        IUser userService;
        IAdmin adminService;
        UserManager<User> userManager;
        public AdminController(UserManager<User> _userManager, IUser _userService, IAdmin _adminService)
        {
            userManager = _userManager;
            userService = _userService;
            adminService = _adminService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return View(await adminService.GetUsers());
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            User user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                await userManager.DeleteAsync(user);
            }
            return RedirectToAction("GetUsers");
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            User user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(User _user)
        {
            if (ModelState.IsValid)
            {
                User user = await adminService.GetUser(_user.Id);
                if (user != null)
                {
                    user.Name = _user.Name;
                    user.Email = _user.Email;
                    user.Surname = _user.Surname;
                    user.DateOfBirth = _user.DateOfBirth;
                    var result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("GetUsers");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                            ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(_user);
        }
    }
}
