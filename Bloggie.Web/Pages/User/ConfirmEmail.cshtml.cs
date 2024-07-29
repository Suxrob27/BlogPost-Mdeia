using DB.Context;
using DB.Model.Notification;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Bloggie.Web.Pages.User
{
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public ConfirmEmailModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            this.signInManager = signInManager;
        }
        public async Task<IActionResult> OnGet(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false); 
                    var notification = new NotificationModel
                    {
                        Message = "Email Address is successfully confirmed, you can now try to login",
                        Type = NotificationType.Success,
                    };
                    TempData["Notification"] = JsonSerializer.Serialize(notification);
                    return RedirectToPage("/Index");
                }
                else
                {
                    var notification = new NotificationModel
                    {
                        Message = "Sorry But Some Errors Happened",
                        Type = NotificationType.Error,
                    };
                    TempData["Notification"] = JsonSerializer.Serialize(notification);
                    return RedirectToPage("/Index");
                }
            }
            else
            {
                var notification = new NotificationModel
                {
                    Message = "Sorry But There no so kind of User here",
                    Type = NotificationType.Error,
                };
                TempData["Notification"] = JsonSerializer.Serialize(notification);
                return RedirectToPage("/Index");
            }
        }
    }
}
