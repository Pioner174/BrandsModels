using BrandsModels.Models;
using BrandsModels.Models.EFModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Drawing2D;

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
        public IActionResult Brands()
        {
            IEnumerable<Brand> brands = _context.Brands;
            return View(brands);
        }

        [HttpGet("Data/EditOrCreateBrand/{id?}")]
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> EditOrCreateBrand([FromForm] Brand brand)
        {
            var bdBrand = await _context.Brands.FirstOrDefaultAsync(b=> b.Id == brand.Id);

            if(bdBrand == null)
            {
                brand.Id = 0;
                await _context.Brands.AddAsync(brand);
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

        [HttpGet]
        public async Task<IActionResult> Models(bool group = false, int page = 1, int pageSize = 15)
        {
            IQueryable<Model> modelsQuery = _context.Models.Include(m => m.Brand);

            if(group)
                modelsQuery = modelsQuery.OrderBy(m => m.Brand.Name);

            var models = await modelsQuery.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            var totalCount = modelsQuery.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            ViewBag.Page = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.GroupBy = group;

            return View(models);
        }

        [HttpGet("Data/EditOrCreateModel/{id?}")]
        [Authorize]
        public async Task<IActionResult> EditOrCreateModel(int? id)
        {
            List<Brand> Brands = await _context.Brands.ToListAsync();

            ViewBag.Brands = Brands;

            if (id == null)
            {
                return View(new Model());
            }

            var model = await _context.Models.FindAsync(id);

            if (model == null)
            {
                return View(new Model());
            }
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditOrCreateModel([FromForm] Model model)
        {
            var bdmodel = await _context.Models.FirstOrDefaultAsync(b => b.Id == model.Id);

            if (bdmodel == null)
            {
                model.Id = 0;
                await _context.Models.AddAsync(model);
            }
            else
            {
                bdmodel.Name = model.Name;
                bdmodel.Brand = await _context.Brands.FirstOrDefaultAsync(b => b.Id == model.BrandId) ?? bdmodel.Brand;
                bdmodel.IsActive = model.IsActive;

                _context.Models.Update(bdmodel);
            }
            await _context.SaveChangesAsync();

            return RedirectToAction("Models");
        }

    }
}
