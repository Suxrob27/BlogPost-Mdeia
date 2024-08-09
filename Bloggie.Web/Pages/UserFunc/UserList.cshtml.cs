using DB.Context;
using DB.Model.Notification;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Bloggie.Web.Pages.UserFunc
{
    public class UserListModel : PageModel
    {
        private readonly AuthDb _dB;
        private readonly UserManager<ApplicationUser> userManager;

        public List<ApplicationUser> Users { get; set; }    

        public UserListModel(AuthDb dB,UserManager<ApplicationUser> userManager)
        {
            _dB = dB;
            this.userManager = userManager;
        }


        public async Task<IActionResult> OnGet()
        {
            var notification = (string)TempData["Notification"];
            if (notification != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<NotificationModel>(notification);
            }
            Users = _dB.applicationUser.ToList();
            foreach (var user in Users)
            {
                var user_role = await userManager.GetRolesAsync(user);
                user.Role = string.Join(',', user_role);

                var user_claim = userManager.GetClaimsAsync(user).GetAwaiter().GetResult().Select(u => u.Type);
                user.UserClaim = string.Join(',', user_claim);
            }
            return Page();
        }
    }
}
