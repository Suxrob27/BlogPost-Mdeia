using DB.Context;
using DB.Model.Notification;
using DB.Model.TwoFactorAuth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Bloggie.Web.Pages.TwoFactorAuthentication
{
    public class EnableAuthenticationModel : PageModel
    {
        [BindProperty]
      public TFAAuthenticationViewModel model { get; set; } 

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly UrlEncoder _urlEncoder;

        public EnableAuthenticationModel(UserManager<ApplicationUser> userManager, UrlEncoder urlEncoder)
        {
            _userManager = userManager;
            _urlEncoder = urlEncoder;
        }
        public async Task<IActionResult> OnGet()
        {
            string AuthenticatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";
            var user = await _userManager.GetUserAsync(User);
            await _userManager.ResetAuthenticatorKeyAsync(user);
            var token = await _userManager.GetAuthenticatorKeyAsync(user);
             
            string AuthUri = string.Format(AuthenticatorUriFormat, _urlEncoder.Encode("IdentityManager"),
              _urlEncoder.Encode(user.Email), token);
            model = new TFAAuthenticationViewModel() { Token = token ,QrCodeUrl = AuthUri};
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var successed = await _userManager.VerifyTwoFactorTokenAsync(user, _userManager.Options.Tokens.AuthenticatorTokenProvider, model.Code);
                if (successed)
                {
                    await _userManager.SetTwoFactorEnabledAsync(user, true);
                    var notification = new NotificationModel()
                    {
                        Message = "Congratullatons The Two Factor authentication has Been Set Up  Successfully",
                        Type = NotificationType.Success
                    };
                    TempData["Notification"] = JsonSerializer.Serialize(notification);
                    return RedirectToPage("/Index");
                }
                else
                {
                    var notification = new NotificationModel()
                    {
                        Message = "Somthing Went Wrong Bro...!Try it Again or Later",
                        Type = NotificationType.Error  
                    };
                    TempData["Notification"] = JsonSerializer.Serialize(notification);
                    return RedirectToPage("/Index");

                }

            }

            var notifications = new NotificationModel()
            {
                Message = "Somthing Went Wrong Bro...!Try it Again or Later",
                Type = NotificationType.Error
            };
            TempData["Notification"] = JsonSerializer.Serialize(notifications);
            return RedirectToPage("/Index");
        }
    }
}
