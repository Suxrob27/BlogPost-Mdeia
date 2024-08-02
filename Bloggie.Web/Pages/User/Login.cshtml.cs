using Bloggie.Web.Pages.TwoFactorAuthentication;
using DB.Context;
using DB.Model.Notification;
using DB.Model.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Bloggie.Web.Pages.User
{
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
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
            var notificationJson = (string)TempData["Notification"];
            if (notificationJson != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<NotificationModel>(notificationJson);
            }
        }

        public async Task<IActionResult> OnPost()
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
                    return RedirectToPage("/Index");
                }

                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("/TwoFactorAuthentication/VerifyAuthenticatorCode", new { rememberMe = model.RememberMe  });
                }
                if (result.IsLockedOut)
                {
                    var notification = new NotificationModel()
                    {
                        Message = "Sorry But Your Account Has Been Locked Try it Again",
                        Type = NotificationType.Error,
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
                    Type = NotificationType.Error,
                };
                TempData["Notification"] = JsonSerializer.Serialize(notification);
                return RedirectToPage("/User/Registration");
            }
            return Page();
        }
    }
}
