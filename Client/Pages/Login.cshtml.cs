using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Client.Pages
{
    public class LoginModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync(string redirectUrl)
        {
            if (string.IsNullOrWhiteSpace(redirectUrl))
            {
                redirectUrl = Url.Content("~/");
            }

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                Response.Redirect(redirectUrl);
            }

            return Challenge(new AuthenticationProperties
            {
                RedirectUri = redirectUrl,
            },
            OpenIdConnectDefaults.AuthenticationScheme);
        }
    }
}
