using Bloggie.Web.Pages.User;
using DB;
using DB.Context;
using DB.Model.Notification;
using DB.Model.User;
using DB.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

namespace Bloggie.Web.Pages.UserFunc
{
    [Authorize(Policy = "superAdmin")]

    public class UserListModel : PageModel
    {
        private readonly AuthDb _dB;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public List<ApplicationUser> Users { get; set; }
        [BindProperty]
        public RegistrationViewModel registrationModel { get; set; }

        public UserListModel(AuthDb dB,UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _dB = dB;
            _userManager = userManager;
            this.roleManager = roleManager;
        }


        public async Task<IActionResult> OnGet()
        {
            registrationModel = new()
            {
                RoleList = roleManager.Roles.Select(x => x.Name).Select(i => new SelectListItem
                { 
                  Text = i,
                  Value = i
                })
            };
            var notification = (string)TempData["Notification"];
            if (notification != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<NotificationModel>(notification);
            }
            Users = _dB.applicationUser.ToList();
            foreach (var user in Users)
            {
                var user_role = await _userManager.GetRolesAsync(user);
                user.Role = string.Join(',', user_role);

                var user_claim = _userManager.GetClaimsAsync(user).GetAwaiter().GetResult().Select(u => u.Type);
                user.UserClaim = string.Join(',', user_claim);
            }
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            if (registrationModel == null)
            {
                var notification = new NotificationModel()
                {  Message = "Bro Sorry, But There iss no User with this Data",
                    Type = NotificationType.Error   
                };
                TempData["Notification"] = JsonSerializer.Serialize(notification);  
            }
            if (ModelState.IsValid)
            {

                var user = new ApplicationUser()
                {
                    Email = registrationModel.Email,
                    Name = registrationModel.Name,
                    UserName = registrationModel.Email,
                    DateCreated = DateTime.Now,
                };
                var result = await _userManager.CreateAsync(user, registrationModel.Password);
                if (result.Succeeded)
                {
                    if (registrationModel.RoleSelected != null)
                    {
                        await _userManager.AddToRoleAsync(user, registrationModel.RoleSelected);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, SD.User);
                    }
                    var notification = new NotificationModel()
                    {
                        Message = "User Has Been Created SuccessFully",
                        Type = NotificationType.Success,    
                    };
                    TempData["Notification"] = JsonSerializer.Serialize(notification);
                    return RedirectToPage("/UserFunc/UserList");
                }
                var notifications2 = new NotificationModel()
                {
                    Message = "Smth Went Wrong Bro. Try It Again Or Later",
                    Type = NotificationType.Error,
                };
                TempData["Notification"] = JsonSerializer.Serialize(notifications2);
                return RedirectToPage("/UserFunc/UserList");
            }
            var notifications3 = new NotificationModel()
            {
                Message = "Smth Went Wrong Bro. Try It Again Or Later",
                Type = NotificationType.Error,
            };
            TempData["Notification"] = JsonSerializer.Serialize(notifications3);
            return RedirectToPage("/UserFunc/UserList");


        }

    }
}
