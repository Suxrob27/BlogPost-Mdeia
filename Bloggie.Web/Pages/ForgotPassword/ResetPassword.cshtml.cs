using DB.Context;
using DB.Model;
using DB.Model.Notification;
using DB.Model.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Bloggie.Web.Pages.ForgotPassword
{
    public class ResetPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        [BindProperty]
        public ResetPasswordViewModel model { get; set; }
        public ResetPasswordModel(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult OnGet(string code = null)
        {
            if (code == null)
            {
                var notification = new NotificationModel()
                {
                    Message = "Smth Went Wrong Bor.Try It Again Or Try it Later",
                    Type = NotificationType.Error
                };
                TempData["Notification"] = JsonSerializer.Serialize(notification);
                return RedirectToPage("/User/Login");
            }
            return Page();
        }

        public async Task<IActionResult> OnPost(string code = null)
        {
            
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                var result = await userManager.ResetPasswordAsync(user, code, model.Password);
                if (result.Succeeded)
                {
                    await signInManager.PasswordSignInAsync(model.Email, model.ConfirmPassword, isPersistent: true, lockoutOnFailure: true);

                    var notification = new NotificationModel()
                    {
                        Message = "Congratullations The Password has been Changed successfully ",
                        Type = NotificationType.Success,
                    };


                    TempData["Notification"] = JsonSerializer.Serialize(notification);
                    return RedirectToPage("/Index");
                }
                else
                {
                    var notification = new NotificationModel()
                    {
                        Message = "Something Went Wrong.Try It Again Or Try It Later",
                        Type = NotificationType.Error
                    };
                    TempData["Notification"] = JsonSerializer.Serialize(notification);
                    return RedirectToPage("/User/Login");

                }
            }
            else
            {
                var notification = new NotificationModel()
                {
                    Message = "Something Went Wrong.Try It Again Or Try It Later",
                    Type = NotificationType.Error
                };
                TempData["Notification"] = JsonSerializer.Serialize(notification);
                return RedirectToPage("/User/Login");
            }
        }
    }
}
