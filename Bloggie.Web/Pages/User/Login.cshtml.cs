using DB.Context;
using DB.Model.Notification;
using DB.Model.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Bloggie.Web.Pages.User
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public LoginViewModel model { get; set; }

        private readonly SignInManager<ApplicationUser> signInManager;

        public LoginModel(SignInManager<ApplicationUser> signInManager)
        {
            this.signInManager = signInManager;
        }


        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    var notifcation = new NotificationModel()
                    {
                        Message = "Congratullations You have successfully signed up to this web site",
                        Type = NotificationType.Success,
                    };
                    return RedirectToPage(returnUrl);
                }

                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("/User/TwoFactorAuth");
                }
                if (result.IsLockedOut)
                {
                    var notification = new NotificationModel()
                    {
                        Message = "Sorry But Your Account Has Been Locked Try it Again",
                        Type = NotificationType.Info,
                    };
                    TempData["Notification"] = JsonSerializer.Serialize(notification);
                    return RedirectToPage("/Index");
                }
            }
            else
            {
                var notification = new NotificationModel()
                {
                    Message = "We Could Not Find so kind Of Acccount. It Seems that You need to Register First",
                    Type = NotificationType.Info,
                };
                TempData["Notification"] = JsonSerializer.Serialize(notification);
                return RedirectToPage("/User/Registration");
            }
            return Page();
        }
    }
}
