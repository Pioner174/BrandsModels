using BrandsModels.Models.EFModels;
using Microsoft.EntityFrameworkCore;

namespace BrandsModels.Models
{
    public static class SeedData
    {

        public static void SeedDatabase(DataContext contex)
        {

            contex.Database.Migrate();


            if (contex.Brands.Count() == 0)
            {
                Brand b1 = new Brand() { Name = "BMW" };
                Brand b2 = new Brand() { Name = "Toyota" };
                Brand b3 = new Brand() { Name = "Nissan" };
                Brand b4 = new Brand() { Name = "KIA" };
                Brand b5 = new Brand() { Name = "Honda" };
                Brand b6 = new Brand() { Name = "Skoda" };
                Brand b7 = new Brand() { Name = "Lada" };
                Brand b8 = new Brand() { Name = "Mazda" };

                contex.Brands.AddRange(b1, b2, b3, b4, b5, b6, b7, b8);
                contex.SaveChanges();

                contex.Models.AddRange(
                    new Model
                    {
                        Name = "X1",
                        Brand = b1
                    },
                    new Model
                    {
                        Name = "X2",
                        Brand = b1
                    },
                    new Model
                    {
                        Name = "X3",
                        Brand = b1
                    },
                    new Model
                    {
                        Name = "X4",
                        Brand = b1
                    },
                    new Model
                    {
                        Name = "X5",
                        Brand = b1
                    },
                    new Model
                    {
                        Name = "X6",
                        Brand = b1
                    },
                    new Model
                    {
                        Name = "C-HR",
                        Brand = b2
                    },
                    new Model
                    {
                        Name = "Supra",
                        Brand = b2
                    },
                    new Model
                    {
                        Name = "Corolla",
                        Brand = b2
                    },
                    new Model
                    {
                        Name = "Rav4",
                        Brand = b2
                    },
                    new Model
                    {
                        Name = "Camry",
                        Brand = b2
                    },
                    new Model
                    {
                        Name = "Ariya",
                        Brand = b3
                    },
                    new Model
                    {
                        Name = "Qashqai",
                        Brand = b3
                    },
                    new Model
                    {
                        Name = "Murano",
                        Brand = b3
                    },
                    new Model
                    {
                        Name = "Terrano",
                        Brand = b3
                    },
                    new Model
                    {
                        Name = "X-Trail",
                        Brand = b3
                    },
                    new Model
                    {
                        Name = "K5",
                        Brand = b4
                    },
                    new Model
                    {
                        Name = "Rio",
                        Brand = b4
                    },
                    new Model
                    {
                        Name = "Cerato",
                        Brand = b4
                    },
                    new Model
                    {
                        Name = "K900",
                        Brand = b4
                    },
                    new Model
                    {
                        Name = "Cee'd",
                        Brand = b4
                    },
                    new Model
                    {
                        Name = "Accord",
                        Brand = b5
                    },
                    new Model
                    {
                        Name = "CR-V",
                        Brand = b5
                    },
                    new Model
                    {
                        Name = "Civic",
                        Brand = b5
                    },
                    new Model
                    {
                        Name = "Capa",
                        Brand = b5
                    },
                    new Model
                    {
                        Name = "Concerto",
                        Brand = b5
                    },
                    new Model
                    {
                        Name = "Rapid",
                        Brand = b6
                    },
                    new Model
                    {
                        Name = "Superb",
                        Brand = b6
                    },
                    new Model
                    {
                        Name = "Octavia",
                        Brand = b6
                    },
                    new Model
                    {
                        Name = "Fabia",
                        Brand = b6
                    },
                    new Model
                    {
                        Name = "Karoq",
                        Brand = b6
                    },
                    new Model
                    {
                        Name = "Vesta",
                        Brand = b7
                    },
                    new Model
                    {
                        Name = "Granta",
                        Brand = b7
                    },
                    new Model
                    {
                        Name = "Niva",
                        Brand = b7
                    },
                    new Model
                    {
                        Name = "XRAY",
                        Brand = b7
                    },
                    new Model
                    {
                        Name = "Largus",
                        Brand = b7
                    },
                    new Model
                    {
                        Name = "6",
                        Brand = b8
                    },
                    new Model
                    {
                        Name = "CX-5",
                        Brand = b8
                    },
                    new Model
                    {
                        Name = "RX-6",
                        Brand = b8
                    },
                    new Model
                    {
                        Name = "CX-9",
                        Brand = b8
                    },
                    new Model
                    {
                        Name = "CX-30",
                        Brand = b8
                    }
                    );

                contex.SaveChanges();
            }
        }
    }
}
