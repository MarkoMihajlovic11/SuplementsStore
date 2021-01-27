using SuplementsStore1.Data;
using System.Collections.Generic;
using System.Linq;

namespace SuplementsStore1.Models
{
    //klasa koja ce da pokpi podatke uz pomoc EF Core-a
    public class EFProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext context;

        public EFProductRepository(ApplicationDbContext context)
        {
            this.context = context;
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
                context.Products.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
