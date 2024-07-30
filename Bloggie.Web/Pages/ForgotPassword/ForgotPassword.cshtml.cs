using DB.Context;
using DB.IRepository;
using DB.Model.Notification;
using DB.Model.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Bloggie.Web.Pages.ForgotPassword
{
    public class ForgotPasswordModel : PageModel
    {
        [BindProperty]
        public ForgotPasswordModelView model { get; set; }  

        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmailServise _emailSender;

        public ForgotPasswordModel(UserManager<ApplicationUser> userManager, IEmailServise emailSender)
        {
            this.userManager = userManager;
           _emailSender = emailSender;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                var notification = new NotificationModel()
                {
                    Message = "Sorry But We Could not Find User With This Email",
                    Type = NotificationType.Error
                };
                TempData["Notification"] = JsonSerializer.Serialize(notification);
                return RedirectToPage("/User/Login");
            }
            else
            {
                var code = await userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.PageLink("/ForgotPassword/ResetPassword", values: new
                {
                    userid = user.Id,
                    code
                },protocol: HttpContext.Request.Scheme);

                 await _emailSender.Send("suxrobvjl1@gmail.com",model.Email, "Reset Password - Identity Manager",
                                       $"Please reset your password by clicking here: {callbackUrl}");
                var notification = new NotificationModel()
                {
                    Message = "Now Go And Check Your Email To continue resetting Password",
                    Type = NotificationType.Success,
                };
                TempData["Notification"] = JsonSerializer.Serialize(notification);
                return RedirectToPage("/User/Login");
            }
        }
    }
}
