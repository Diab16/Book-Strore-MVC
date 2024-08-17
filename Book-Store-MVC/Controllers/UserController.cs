using AutoMapper;
using Book_Store_MVC.IRepositories;
using Book_Store_MVC.Models;
using Book_Store_MVC.ViewModels;
using Day2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace Book_Store_MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository genericUser;
        private readonly IMapper mapper;
        BookStoreContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public UserController(IUserRepository genericUser, IMapper mapper ,BookStoreContext context ,UserManager<ApplicationUser> userManager)
        {

            this.genericUser = genericUser;
            this.mapper = mapper;
            this.context = context;
            this.userManager = userManager;
        }


        // GET: ApplicationUserController
        public ActionResult Index()
        {
            List<ApplicationUser> users = genericUser.GetAll().ToList();
            return View(users);
        }




        public ActionResult Create()
        {
            UserManagerViewModel userVM = new UserManagerViewModel();
            userVM.Roles = context.Roles.ToList();
            return View(userVM);
        }


        [HttpPost]

        public async Task<ActionResult> Create(UserManagerViewModel userVM)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser userDB = new ApplicationUser()
                {
                    UserName = userVM.Username,
                    Email = userVM.UserEmail,
                    PasswordHash = userVM.Password,
                    Address = userVM.Address,
                };
                IdentityResult result = await userManager.CreateAsync(userDB, userVM.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(userDB, userVM.roleName);
                    Claim Roleclaim = new Claim("role", userVM.roleName);
                    await userManager.AddClaimAsync(userDB, Roleclaim);
                    return RedirectToAction(nameof(Index));
                }
                foreach (var item in result.Errors)
                    ModelState.AddModelError("", item.Description);

                return RedirectToAction(nameof(Index));
            }
            userVM.Roles = context.Roles.ToList();
            return View(userVM);


        }


        public ActionResult Edit(string id)
        {
            var user = genericUser.GetByStringId(id);
            if(user != null)
            {
                
                string rolename = userManager.GetRolesAsync(user).Result[0];
                string roleid = context.Roles.FirstOrDefault(r => r.Name == rolename).Id;
                UserManagerViewModel userVM = new UserManagerViewModel()
                {
                    Username = user.UserName,
                    UserEmail = user.Email,
                    Password = string.Empty,
                    ConfirmPassword = string.Empty,
                    Address = user.Address,
                    roleId = roleid,
                    roleName = rolename,
                    Role = context.Roles.FirstOrDefault(r => r.Id == roleid),
                    Roles = context.Roles.ToList(),

                };
                if (ModelState.IsValid)
                {   
                    ViewBag.userId = user.Id;
                    return View(userVM);

                }
                userVM.Roles = context.Roles.ToList();
                return View(user.Id);

                
            }
            return RedirectToAction(nameof(Index));
         }


        [HttpPost]
        public async Task<ActionResult> Edit(UserManagerViewModel userVM)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser userdb = context.Users.FirstOrDefault(u => u.Email == userVM.UserEmail);
                if(userdb != null)
                {
                    await userManager.DeleteAsync(userdb);

                    if (ModelState.IsValid)
                    {
                        ApplicationUser userDB = new ApplicationUser()
                        {
                            UserName = userVM.Username,
                            Email = userVM.UserEmail,
                            PasswordHash = userVM.Password,
                            Address = userVM.Address,
                        };
                        IdentityResult result = await userManager.CreateAsync(userDB, userVM.Password);
                        if (result.Succeeded)
                        {
                            await userManager.AddToRoleAsync(userDB, userVM.roleName);
                            Claim Roleclaim = new Claim("role", userVM.roleName);
                            await userManager.AddClaimAsync(userDB, Roleclaim);
                            var s = userManager.GetUserId(User);
                            if (userdb.Id == userManager.GetUserId(User))
                            {
                                return RedirectToAction("signout", "account");
                            }
                            return RedirectToAction(nameof(Index));
                        }
                        foreach (var item in result.Errors)
                            ModelState.AddModelError("", item.Description);
                        
                        return RedirectToAction(nameof(Index));
                    }
                }
                userVM.Roles = context.Roles.ToList();
                return View(userVM);
                
            }
            userVM.Roles = context.Roles.ToList();
            return View(userVM);

        }

        public ActionResult Delete(string id)
        {
            var user = genericUser.GetByStringId(id);
            if (user != null)
            {
                
                string rolename = userManager.GetRolesAsync(user).Result[0];
                string roleid = context.Roles.FirstOrDefault(r => r.Name == rolename).Id;
                DeleteUser userVM = new DeleteUser()
                {
                    Username = user.UserName,
                    UserEmail = user.Email,
                    Password = "********",
                    ConfirmPassword = "********",
                    Address = user.Address,
                    roleId = roleid,
                    roleName = rolename,
                    Role = context.Roles.FirstOrDefault(r => r.Id == roleid),
                    Roles = context.Roles.ToList(),

                };
                ViewBag.userId = user.Id;
                return View(userVM);
            }
            
            return View(user);
        }

        [HttpPost]

        public async Task<ActionResult> DeleteUser(string emailVM)
        {
            ApplicationUser userdb = context.Users.FirstOrDefault(u => u.Email == emailVM);
            if (userdb != null)
            {

                    await userManager.DeleteAsync(userdb);
                    return RedirectToAction(nameof(Index));

                
                
            }
            return Delete(userdb.Id);
        }
    }
}
