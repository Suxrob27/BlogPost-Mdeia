using DB.Context;
using DB.Model.Notification;
using DB.Model.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security;
using System.Text.Json;

namespace Bloggie.Web.Pages.UserFunc
{
    public class ManageRoleModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        [BindProperty]
        public RolesViewModel model { get; set; }
        

        public ManageRoleModel(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public async Task<IActionResult> OnGet(string userId)
        {
            ApplicationUser user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                var notification = new NotificationModel()
                {
                    Message = "Bro s+orry, but So Kind Of User Could not be found",
                    Type = NotificationType.Error
                };
                TempData["Notififcation"] = JsonSerializer.Serialize(notification);
                return RedirectToPage("/UsserFunc/UserList");
            }
            List<string> exsitingRoles = await userManager.GetRolesAsync(user) as List<string>;
             model = new RolesViewModel()
            {
              User = user,
            };
            foreach (var role in roleManager.Roles)
            {
                RoleSelection roleSelection = new()
                {
                    RoleName = role.Name
                };
                if (exsitingRoles.Any(x => x == role.Name))
                {
                    roleSelection.IsSelected = true;
                }
               
                model.RoleList.Add(roleSelection);  
            }
            return Page();
        }
        


        public  async Task<IActionResult> OnPost(string  userId)
        {
            ApplicationUser user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                var notification = new NotificationModel()
                {
                    Message = "Bro s+orry, but So Kind Of User Could not be found",
                    Type = NotificationType.Error
                };
                TempData["Notififcation"] = JsonSerializer.Serialize(notification);
                return RedirectToPage("/UserFunc/UserList");
            }

            var oldUSerRole = await userManager.GetRolesAsync(user);
            var resutl = await userManager.RemoveFromRolesAsync(user, oldUSerRole);
            if (!resutl.Succeeded) 
            {
                var notification = new NotificationModel()
                {
                    Message = "Bro sorry, but smth went wrong. Try it again Or Later either wrtie your problem to support.",
                    Type = NotificationType.Error
                };
                TempData["Notififcation"] = JsonSerializer.Serialize(notification);
                return RedirectToPage("/UserFunc/UserList");
            }
            resutl = await userManager.AddToRolesAsync(user, model.RoleList.Where(x => x.IsSelected).Select(x =>x.RoleName));

            if (!resutl.Succeeded)
            {
                var notification = new NotificationModel()
                {
                    Message = "Bro sorry, but smth went wrong. Try it again Or Later either wrtie your problem to support.",
                    Type = NotificationType.Error
                };
                TempData["Notififcation"] = JsonSerializer.Serialize(notification);
                return RedirectToPage("/UserFunc/UserList");
            }
            else
            {
                var notification = new NotificationModel()
                {
                    Message = "Roles assigned successfully",
                    Type = NotificationType.Error
                };
                TempData["Notififcation"] = JsonSerializer.Serialize(notification);
                return RedirectToPage("/UserFunc/UserList");
            }
        }
    }
    }

