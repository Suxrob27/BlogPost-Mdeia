using DB.Context;
using DB.Model.Notification;
using DB.Model.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Text.Json;

namespace Bloggie.Web.Pages.UserFunc
{
    public class ManageClaimsModel : PageModel
    {
        [BindProperty]
        public ClaimsciewModel model { get; set; }
        private readonly UserManager<ApplicationUser> userManager;

        public ManageClaimsModel(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<IActionResult> OnGet(string userId)
        {
            ApplicationUser user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                var notification = new NotificationModel()
                {
                    Message = "Bro s+orry, but So Kind Of User Could not be found",
                    Type = NotificationType.Error
                };
                TempData["Notification"] = JsonSerializer.Serialize(notification);
                return RedirectToPage("/UsserFunc/UserList");
            }
            var exsitingClaim = await userManager.GetClaimsAsync(user);
            model = new ClaimsciewModel()
            {
                User = user,
            };
            foreach (Claim claim in ClaimStore.claimsList)
            {
                ClaimSelection roleSelection = new()
                {
                    ClaimType = claim.Value,
                };
                if (exsitingClaim.Any(x => x.Type == claim.Value))
                {
                    roleSelection.IsSelected = true;
                }

                model.ClaimList.Add(roleSelection);
            }
            return Page();
        }
        public async Task<IActionResult> OnPost(string userId)
        {
            ApplicationUser user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                var notification = new NotificationModel()
                {
                    Message = "Bro s+orry, but So Kind Of User Could not be found",
                    Type = NotificationType.Error
                };
                TempData["Notification"] = JsonSerializer.Serialize(notification);
                return RedirectToPage("/UserFunc/UserList");
            }

            var oldUSerClaims = await userManager.GetClaimsAsync(user);
            var resutl = await userManager.RemoveClaimsAsync(user, oldUSerClaims);
            if (!resutl.Succeeded)
            {
                var notification = new NotificationModel()
                {
                    Message = "Bro sorry, but smth went wrong. Try it again Or Later either wrtie your problem to support.",
                    Type = NotificationType.Error
                };
                TempData["Notification"] = JsonSerializer.Serialize(notification);
                return RedirectToPage("/UserFunc/UserList");
            }
            resutl = await userManager.AddClaimsAsync(user, model.ClaimList.Where(x => x.IsSelected).Select(x => new Claim(x.ClaimType, x.IsSelected.ToString())));

            if (!resutl.Succeeded)
            {
                var notification = new NotificationModel()
                {
                    Message = "Bro sorry, but smth went wrong. Try it again Or Later either wrtie your problem to support.",
                    Type = NotificationType.Error
                };
                TempData["Notification"] = JsonSerializer.Serialize(notification);
                return RedirectToPage("/UserFunc/UserList");
            }
            else
            {
                var notification = new NotificationModel()
                {
                    Message = "Claims assigned successfully",
                    Type = NotificationType.Success
                };
                TempData["Notification"] = JsonSerializer.Serialize(notification);
                return RedirectToPage("/UserFunc/UserList");
            }
        }
     
    }
}
