using DB.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.Web.Pages.User
{
    public class LogOffModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signInManager;

        public LogOffModel(SignInManager<ApplicationUser> signInManager)
        {
            this.signInManager = signInManager;
        }
        public async Task<IActionResult> OnGet()
        {
            await  signInManager.SignOutAsync();
            return RedirectToPage("/Index");
        }
    }
}
