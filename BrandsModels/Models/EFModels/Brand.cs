using System.ComponentModel.DataAnnotations;

namespace BrandsModels.Models.EFModels
{
    public class Brand : BaseEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите название Бренда")]
        public string Name { get; set; }
    }
}
