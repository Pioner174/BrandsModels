using BrandsModels.Models.ViewModels;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BrandsModels.Controllers
{

    [AllowAnonymous]
    public class AccountController : Controller
    {
        private SignInManager<IdentityUser> _signinManager;

        private UserManager<IdentityUser> _userManager;

        private IConfiguration _configuration;

        public AccountController(SignInManager<IdentityUser> signinManager, UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _signinManager = signinManager;
            _userManager = userManager;
            this._configuration = configuration;
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

        [HttpGet("Account/Login/{returnUrl?}")]
        public IActionResult SignIn(string returnUrl)
        {
            return View(new SignInViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost("Account/Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn([FromForm] SignInViewModel viewModel)
        {
            IdentityUser user = await _userManager.FindByEmailAsync(viewModel.Name) ?? await _userManager.FindByNameAsync(viewModel.Name);

            if (user != null)
            {
                var result = await _signinManager.PasswordSignInAsync(user, viewModel.Password, false, false);

                if (result.Succeeded)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_configuration["jwtSecret"]);

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Email, user.Email)
                        }),
                        Expires = DateTime.UtcNow.AddDays(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var tokenString = tokenHandler.WriteToken(token);

                    var cookieOptions = new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(1),
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict
                    };

                    Response.Cookies.Append("token", tokenString, cookieOptions);
                    Response.Cookies.Append("UserName", user.UserName);

                    

                    return Redirect(viewModel?.ReturnUrl ?? "/");
                }
                else
                {
                    ModelState.AddModelError("", "Неправельный логин или пароль");

                    return View(viewModel);
                }

            }
            ModelState.AddModelError("", "Пользователя с таким логином или почтой не обнаруженн");

            return View(viewModel);
        }

        [Authorize]
        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await _signinManager.SignOutAsync();


            Response.Cookies.Delete("token");
            Response.Cookies.Delete("UserName");

            ViewBag.UserName = null;

            return Redirect(returnUrl);
        }

    }
}
