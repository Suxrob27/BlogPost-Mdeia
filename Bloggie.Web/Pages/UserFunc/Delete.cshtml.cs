using DB.Context;
using DB.Model.Notification;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Bloggie.Web.Pages.UserFunc
{
    public class DeleteModel : PageModel
    {
        private readonly AuthDb db;
        private readonly UserManager<ApplicationUser> userManager;

        public DeleteModel(AuthDb db,UserManager<ApplicationUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }
        public async Task<IActionResult> OnPost(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null) 
            {
                var notification = new NotificationModel()
                {
                    Message = "The User Could Not Been Found",
                    Type = NotificationType.Error
                };
                TempData["Notififcation"] = JsonSerializer.Serialize(notification);
            }
            db.applicationUser.Remove(user);    
            db.SaveChanges();
            return RedirectToPage("/UserFunc/UserList");

        }
    }
}
