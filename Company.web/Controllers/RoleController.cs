using Company.Data.Entities;
using Company.web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.web.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RoleController : Controller
    {
        public RoleManager<IdentityRole> _RoleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager , UserManager<ApplicationUser> userManager)
        {
            _RoleManager = roleManager;
            _userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            var roles = await _RoleManager.Roles.ToListAsync();
            return View(roles);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole role)
        {
            if (ModelState.IsValid) 
            {
                var result = await _RoleManager.CreateAsync(role);
                if (result.Succeeded) 
                {
                    return RedirectToAction("Index");
                }
            }
            return View(role);
        }

        public async Task<IActionResult> Details(string Id)
        {
            var role = await _RoleManager.FindByIdAsync(Id);
            if (role == null)
            {
                return NotFound();
            }

            var model = new RoleUpdateViewModel
            {
                Id = role.Id,
                Name = role.Name
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Update(string Id, RoleUpdateViewModel model)
        {
            if (Id != model.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var role = await _RoleManager.FindByIdAsync(Id);
                    if (role == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        role.Name = model.Name;
                        role.NormalizedName = model.Name.ToUpper();

                        var result = await _RoleManager.UpdateAsync(role);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index");
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var role = await _RoleManager.FindByIdAsync(id);
                if (role == null)
                {
                    return NotFound();
                }
                var result = await _RoleManager.DeleteAsync(role);

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


        public async Task<IActionResult> AddOrRemoveUsers(string roleId)
        {
            var role = await _RoleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return NotFound();
            }
            ViewBag.RoleId = roleId;
            var users = await _userManager.Users.ToListAsync();
            var usersInRole = new List<UserInRoleViewModel>();
            foreach (var user in users)
            {
                var userInRole = new UserInRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userInRole.IsSelected = true;
                }
                else
                {
                    userInRole.IsSelected = false;
                }
                usersInRole.Add(userInRole);
            }
            return View(usersInRole);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(string roleId, List<UserInRoleViewModel> users)
        {
            var role = await _RoleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                foreach(var user in users)
                {
                    var appUser = await _userManager.FindByIdAsync(user.UserId);
                    if (appUser is not null)
                    {
                        if(user.IsSelected && await _userManager.IsInRoleAsync(appUser , role.Name))
                        {
                            await _userManager.AddToRoleAsync(appUser , role.Name);
                        }else if (!user.IsSelected && await _userManager.IsInRoleAsync(appUser, role.Name))
                        {
                            await _userManager.RemoveFromRoleAsync(appUser, role.Name);
                        }
                    }
                }
                return RedirectToAction("Index");
            }

            return View(users);
        }

    }
}
