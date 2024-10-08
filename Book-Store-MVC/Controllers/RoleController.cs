﻿using Day2.Models;
using Day2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Day2.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager) 
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        public IActionResult AddRole()
        {
            return View("AddRole");
        }
        [HttpPost]
        
        public async Task<IActionResult> AddRole(RoleViewModel roleVM)
        {
            if(ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole()
                {
                    Name = roleVM.RoleName
                };
                IdentityResult result = new IdentityResult();
                result = await roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("addrole");
                }
                foreach (var item in result.Errors)
                    ModelState.AddModelError("", item.Description);

            }
            return View("AddRole",roleVM);
            
        }

        public IActionResult AssignRole()
        {
            return View("AssignRole");
        }
        [HttpPost]

        public async Task<IActionResult> AssignRole(AssignRoleViewModel roleVM)
        {
            if (ModelState.IsValid)
            {
                IdentityRole result = new IdentityRole();
                result = await roleManager.FindByNameAsync(roleVM.RoleName);
                ApplicationUser user = await userManager.FindByEmailAsync(roleVM.UserEmail);
                if (result != null && user != null)
                {
                    var subRole = userManager.GetRolesAsync(user).Result[0];
                    
                    if (subRole != null)
                    {
                        await userManager.RemoveFromRoleAsync(user, subRole);
                        Claim oldclaim = new Claim("role", subRole);
                        Claim newclaim = new Claim("role", roleVM.RoleName);
                        await userManager.ReplaceClaimAsync(user, oldclaim, newclaim);
                    }
                    else
                    {
                        Claim newclaim = new Claim("role", roleVM.RoleName);
                        await userManager.AddClaimAsync(user, newclaim);
                    }
                    
                    await userManager.AddToRoleAsync(user, roleVM.RoleName);


                    
                  
                    return RedirectToAction("signout", "account");
                }
                ModelState.AddModelError("", "User or Role Not Found");

            }
            return View("AssignRole", roleVM);

        }
    }
}
