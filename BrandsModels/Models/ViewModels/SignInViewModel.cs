using System.ComponentModel.DataAnnotations;

namespace BrandsModels.Models.ViewModels
{
    public class SignInViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
