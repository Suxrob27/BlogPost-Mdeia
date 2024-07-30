using DB.Context;
using DB.Model.Notification;
using DB.Model.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using DB;
using System.Net.Mail;
using DB.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bloggie.Web.Pages.User
{
    public class RegistrationModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailServise emailServise;
        private readonly RoleManager<IdentityRole> roleManager;

        [BindProperty]
        public RegistrationViewModel registrationModel { get; set; }  

        public RegistrationModel(UserManager<ApplicationUser> userManager, IEmailServise emailServise, RoleManager<IdentityRole> roleManager)
        {
            
            _userManager = userManager;
            this.emailServise = emailServise;
            this.roleManager = roleManager;
        }

        public async Task<IActionResult> OnGet()
        {
            if (!roleManager.RoleExistsAsync(SD.Admin).GetAwaiter().GetResult())
            {
                await roleManager.CreateAsync(new IdentityRole(SD.Admin));
                await roleManager.CreateAsync(new IdentityRole(SD.User));
            }
            registrationModel = new()
            {
                RoleList = roleManager.Roles.Select(x => x.Name).Select(i => new SelectListItem 
                {
                  Text = i,
                  Value = i
                })
            };
            return Page();
        }

        public async Task<IActionResult> OnPost()
        { 
          if (registrationModel == null)
          {
                var notification = new NotificationModel()
                {
                    Message = "Bro Sorry, But There iss no User with this Data",
                    Type = NotificationType.Error   
                };
                TempData["Notification"] = JsonSerializer.Serialize(notification);  
          }
            if (ModelState.IsValid)
            {


                var user = new ApplicationUser()
                {
                    Email = registrationModel.Email,
                    Name = registrationModel.Name,
                    UserName = registrationModel.Email,
                    DateCreated = DateTime.Now,
                };
                var result = await _userManager.CreateAsync(user, registrationModel.Password);
                if (result.Succeeded)
                {
                    if (registrationModel.RoleList != null)
                    {
                        await _userManager.AddToRoleAsync(user, registrationModel.RoleSelected);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, SD.Admin);
                    }
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackurl = Url.PageLink("/User/ConfirmEmail", values: new { userId = user.Id, token = code });

                    await emailServise.Send("suxrobvjl1@gmail.com", user.Email, "Please confirm Your Email",
                         $"Please Click on This Link Yo Confirm Your Email Address :{callbackurl}");
                    var notification = new NotificationModel()
                    {
                        Message = "Now Go To Email And Verify Your Account Bro!!!",
                        Type = NotificationType.Success,
                    };
                    TempData["Notification"] = JsonSerializer.Serialize(notification);
                    return RedirectToPage("/Index");
                }
                return RedirectToPage("/Index");
            }
            else
            {
                var notification = new NotificationModel()
                {
                    Message = "Sorry but Something Went Wrong.Try it Again Or Later",
                    Type = NotificationType.Error,
                };
                TempData["Notification"] = JsonSerializer.Serialize(notification);
                return RedirectToPage("/Index");
            }
        }
    }
}
