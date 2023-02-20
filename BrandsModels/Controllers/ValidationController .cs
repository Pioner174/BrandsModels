using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BrandsModels.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ValidationController : Controller
    {
        private UserManager<IdentityUser> _userManager;

        public ValidationController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> UniqueEmail(string? email)
        {
            if (await _userManager.FindByEmailAsync(email) == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {email} уже используется в системе");
            }
        }
    }
}
