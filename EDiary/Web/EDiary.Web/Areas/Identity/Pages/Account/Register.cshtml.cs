namespace EDiary.Web.Areas.Identity.Pages.Account
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        public IActionResult OnGetAsync(string returnUrl = null)
        {
            return this.RedirectToPage("Login");
        }

        public IActionResult OnPost()
        {
            return this.RedirectToPage("Login");
        }
    }
}
