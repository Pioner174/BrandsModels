using System.ComponentModel.DataAnnotations;

namespace BrandsModels.Models.ViewModels
{
    public class SignInViewModel
    {
        [Required(ErrorMessage ="Введите логин или email")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
