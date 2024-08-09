using DB.Context;
using DB.Model.Notification;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Bloggie.Web.Pages.UserFunc
{
    public class LockUnclockModel : PageModel
    {
        private readonly AuthDb db;

        public LockUnclockModel(AuthDb db)
        {
            this.db = db;
        }
        public async Task<IActionResult> OnPost(string userId)
        {
            ApplicationUser user = await db.applicationUser.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                return NotFound();  
            }
            if (user.LockoutEnd != null && user.LockoutEnd > DateTime.Now)
            {
                user.LockoutEnd = DateTime.Now;
                var notification = new NotificationModel()
                {
                    Message = "User unlocked successfully",
                    Type = NotificationType.Success
                };
                TempData["Notififcation"] = JsonSerializer.Serialize(notification);
            }
            else 
            {
                user.LockoutEnd = DateTime.Now.AddYears(1000);
                var notification = new NotificationModel()
                {
                    Message = "User locked successfully",
                    Type = NotificationType.Success
                };
                TempData["Notififcation"] = JsonSerializer.Serialize(notification);
            }
            db.SaveChanges();
            return RedirectToPage("/UserFunc/UserList");

        }
    }
}
