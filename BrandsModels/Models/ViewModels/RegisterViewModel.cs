using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BrandsModels.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Введите Login")]
        [Remote(action: "UniqueLogin", controller: "Validation")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Введите Email")]
        [Remote(action: "UniqueEmail", controller: "Validation")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}
