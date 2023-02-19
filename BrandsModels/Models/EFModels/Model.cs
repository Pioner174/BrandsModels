using System.ComponentModel.DataAnnotations;

namespace BrandsModels.Models.EFModels
{
    public class Model : BaseEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите название Модели")]
        public string Name { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Укажите бренд")]
        public int BrandId { get; set; }
    }
}
