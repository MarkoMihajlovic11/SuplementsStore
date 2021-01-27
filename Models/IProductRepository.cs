using System.Collections.Generic;

namespace SuplementsStore1.Models
{
    //interfejs-nacin za dobavljanje podataka iz baze
    public interface IProductRepository
    {
        public IEnumerable<Product> Products { get; }
        void SaveProduct(Product product); //posle dodavanja edid-addnew product metode, da bi mogao da cuva proizvod
        Product DeleteProduct(int productID); //funkcionalnost za brisanje prozivoda
    }
}
