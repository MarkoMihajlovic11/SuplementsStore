using SuplementsStore1.Data;
using System.Linq;

namespace SuplementsStore1.Models
{
    //popunjavanje baze podataka
    public class SeedData : ISeedData
    {
        private readonly ApplicationDbContext context;

        public SeedData(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void EnsurePopulated()
        {
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                new Product
                {
                    Name = "Whey Protein 2270g, ",
                    Description = "Chocolate flavour, " +
                    "Supports Muscle Development  " ,
                    Price = 56M,
                    Category = "Protein"
                },
                new Product
                {
                    Name = "Mass Gainer 2722g",
                    Description = "Vanila flavour, " +
                    "1930 Calories per dose",
                    Price = 45M,
                    Category = "Protein"
                },
                new Product
                {
                    Name = "Creatine",
                    Description = "Creatine Monohydrate, " +
                    "Unflavored Powder",
                    Price = 16M,
                    Category = "Creatine"
                },
                new Product
                {
                    Name = "No Pump",
                    Description = "The Ultimate Pre-Workout Experience ",
                    Price = 34M,
                    Category = "Creatine"
                },
                new Product
                {
                    Name = "Kre-Alkalyn",
                    Description = "PH - Correct Creatine Monohydrate " ,
                    Price = 25M,
                    Category = "Creatine"
                },
                new Product
                {
                    Name = "Vitamin C 240 capsules",
                    Description = "1000mg of vitamin C per capsule ",
                    Price = 15M,
                    Category = "Vitamin"
                },
                new Product
                {
                    Name = "Multivitamin Complex 120 caps",
                    Description = "Includes Vitamins B12, Iron, Amino Acids, ",
                    Price = 23M,
                    Category = "Vitamin"
                },
                new Product
                {
                    Name = "Omega 3 240 caps",
                    Description = "Molecularly Distilled & Supercritical Extraction, ",
                    Price = 15M,
                    Category = "Vitamin"
                });
                context.SaveChanges();
            }
        }
    }
}
