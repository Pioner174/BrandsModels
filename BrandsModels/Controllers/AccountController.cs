using BrandsModels.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BrandsModels.Controllers
{

    [AllowAnonymous]
    public class AccountController : Controller
    {
        private SignInManager<IdentityUser> _signinManager;

        private UserManager<IdentityUser> _userManager;

        private IConfiguration configuration;

        public AccountController(SignInManager<IdentityUser> signinManager, UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _signinManager = signinManager;
            _userManager = userManager;
            this.configuration = configuration;
        }


        /// <summary>
        /// Получение формы регистрации
        /// </summary>
        /// <returns></returns>
        
        [HttpGet]
        public IActionResult SignUp()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {

                IdentityUser user = new IdentityUser() { UserName = viewModel.Login, Email = viewModel.Email };

                IdentityResult result = await _userManager.CreateAsync(user, viewModel.Password);
                if (result.Succeeded)
                {
                    return Redirect("/");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(viewModel);
        }

        //[AcceptVerbs("GET", "POST")]
        //public IActionResult UniqueEmail(string email)
        //{
        //    if (_userManager.FindByEmailAsync(email) != null)
        //    {
        //        return Json($"{email} уже есть в системе, попробуйте востановить пароль");
        //    }
        //    return Json(true);
        //}
    }
}
