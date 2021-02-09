using Microsoft.AspNetCore.Hosting;
using SuplementsStore1.Data;
using SuplementsStore1.Models.ViewModels;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SuplementsStore1.Models
{
    //klasa koja ce da pokpi podatke uz pomoc EF Core-a
    public class EFProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment hostingEnviroment;

        public EFProductRepository(ApplicationDbContext context,
                                    IWebHostEnvironment hostingEnviroment)
        {
            this.context = context;
            this.hostingEnviroment = hostingEnviroment;
        }
        public IEnumerable<Product> Products => context.Products;


        //metoda dodaje prozivod u repository ako je ID=0
        //ako nije menja postojeci prozivod
        public void SaveProduct(Product product) 
        { 
            if (product.ProductID == 0)
            { 
                context.Products.Add(product);
            } 
            else 
            { 
                Product dbEntry = context.Products
                    .FirstOrDefault(p => p.ProductID == product.ProductID);
                if (dbEntry != null)
                { 
                    dbEntry.Name = product.Name; 
                    dbEntry.Description = product.Description; 
                    dbEntry.Price = product.Price; 
                    dbEntry.Category = product.Category;
                } 
            } 
            context.SaveChanges();
        }

        //metoda brise prozivod
        public Product DeleteProduct(int productID)
        {
            Product dbEntry = context.Products
                .FirstOrDefault(p => p.ProductID == productID);
            if (dbEntry != null)
            {
                if(dbEntry.PhotoPath != null)
                {
                    string filePath = Path.Combine(hostingEnviroment.WebRootPath,
                            "images", dbEntry.PhotoPath);
                    File.Delete(filePath);
                }

                context.Products.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public Product GetProduct(int productId)
        {
            
            return context.Products
                .FirstOrDefault(p => p.ProductID == productId);
        }
    }
}
