using BrandsModels.Models.EFModels;
using Microsoft.EntityFrameworkCore;

namespace BrandsModels.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Model> Models { get; set; }
    }
}
