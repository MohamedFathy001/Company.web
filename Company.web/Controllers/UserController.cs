using AutoMapper.Configuration.Annotations;
using Company.Data.Entities;
using Company.Service.DTO;
using Company.web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.web.Controllers
{
    [Authorize(Roles ="Admin")]
	public class UserController : Controller
	{
		public UserManager<ApplicationUser> _UserManager;
        public UserController(UserManager<ApplicationUser> userManager)
        {
			_UserManager = userManager;
		}


		public async Task<IActionResult> Index(string searchInp)
		{
			List<ApplicationUser> users;
			if (string.IsNullOrEmpty(searchInp))
			{
				users = await _UserManager.Users.ToListAsync();
			}
			else
			{
				users = await _UserManager.Users.Where(u => u.NormalizedEmail.Trim().Contains(searchInp.ToUpper().Trim())).ToListAsync();	
			}
			return View(users);
		}


        public async Task<IActionResult> Details(string Id)
        {
            var user = await _UserManager.FindByIdAsync(Id);
            if (user == null)
            {
                return NotFound();
            }

            var model = new UserUpdateViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
            };

            return View(model);
        }



        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> Update(string Id, UserUpdateViewModel model)
        {
            if (Id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var user = await _UserManager.FindByIdAsync(Id);
                if (user == null)
                {
                    return NotFound();
                }

                user.UserName = model.UserName;
                user.NormalizedUserName = model.UserName.ToUpper();
                user.Email = model.Email;

                var result = await _UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
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

        public async Task<IActionResult> Delete(string id)
		{
			try
			{
				var user = await _UserManager.FindByIdAsync(id);
				if (user == null)
				{
					return NotFound();
				}
				var result = await _UserManager.DeleteAsync(user);

				if (result.Succeeded)
				{
					return RedirectToAction("Index");
				}
			}
			catch (Exception ex)
			{

			}
			return RedirectToAction("Index");
		}

    }
}
