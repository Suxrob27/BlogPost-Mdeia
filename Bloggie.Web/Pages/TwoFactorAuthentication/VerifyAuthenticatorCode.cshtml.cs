using DB.Context;
using DB.Model.Notification;
using DB.Model.TwoFactorAuth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Bloggie.Web.Pages.TwoFactorAuthentication
{
    public class VerifyAuthenticatorCodeModel : PageModel
    {
        [BindProperty]
        public VerifyAuthenticatiorviewModel model { get; set; }
        private readonly SignInManager<ApplicationUser> signInManager;

        public VerifyAuthenticatorCodeModel(SignInManager<ApplicationUser> signInManager)
        {
            this.signInManager = signInManager;
        }
        public async Task<IActionResult> OnGet()
        {
            var user = await signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                var notification = new NotificationModel()
                {
                    Message = "Somthing Went Wrong Bro...!Try it Again or Later",
                    Type = NotificationType.Error
                };
                TempData["Notification"] = JsonSerializer.Serialize(notification);
                return RedirectToPage("/Index");
            }


            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            var result = await signInManager.TwoFactorAuthenticatorSignInAsync(model.Code, model.RememberMe, rememberClient:false);
            if (result.Succeeded)
            {

                var notification = new NotificationModel()
                {
                    Message = "Success Man!!!!",
                    Type = NotificationType.Success
                };
                TempData["Notification"] = JsonSerializer.Serialize(notification);
                return RedirectToPage(model.ReturnUUrl);
            }
            if (result.IsLockedOut)
            {
                var notification = new NotificationModel()
                {
                    Message = "Bro Are You Kidding, your Account is locked out What are you doing wait for a while.Max 30 min.",
                    Type = NotificationType.Info,
                };
                TempData["Notification"] = JsonSerializer.Serialize(notification);
                return RedirectToPage("/User/Login");
            }
            else
            {
                var notification = new NotificationModel()
                {
                    Message = "Invalid Login Attempt Ahi",
                    Type = NotificationType.Error,
                };
                TempData["Notification"] = JsonSerializer.Serialize(notification);
                return RedirectToPage("/User/Login");
            }
        }


    }
}
