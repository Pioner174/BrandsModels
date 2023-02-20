using BrandsModels.Models;
using BrandsModels.Models.EFModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BrandsModels.Controllers
{
    public class DataController : Controller
    {
        private DataContext _context;

        public DataController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Brands()
        {
            IEnumerable<Brand> brands = _context.Brands;
            return View(brands);
        }

        [HttpGet("Data/EditOrCreateBrand/{id?}")]
        [AllowAnonymous]
        public async Task<IActionResult> EditOrCreateBrand(int? id)
        {
            if(id == null)
            {
                return View(new Brand());
            }

            var brand = await _context.Brands.FindAsync(id);

            if(brand == null)
            {
                return View(new Brand());
            }
            return View(brand);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> EditOrCreateBrand([FromForm] Brand brand)
        {
            var bdBrand = await _context.Brands.FirstOrDefaultAsync(b=> b.Name== brand.Name);

            if(bdBrand == null)
            {
                Brand newBrand = new Brand() { Name = brand.Name, IsActive = brand.IsActive };

                await _context.Brands.AddAsync(newBrand);
            }
            else
            {
                bdBrand.Name = brand.Name;
                bdBrand.IsActive = brand.IsActive;

                _context.Brands.Update(bdBrand);
            }
            await _context.SaveChangesAsync();

            return RedirectToAction("Brands");
        }

    }
}
