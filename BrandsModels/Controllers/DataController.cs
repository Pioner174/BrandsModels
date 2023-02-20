using BrandsModels.Models;
using BrandsModels.Models.EFModels;
using Microsoft.AspNetCore.Mvc;

namespace BrandsModels.Controllers
{
    public class DataController : Controller
    {
        private DataContext _context;

        public DataController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Brands()
        {
            IEnumerable<Brand> brands = _context.Brands;
            return View(brands);
        }
    }
}
