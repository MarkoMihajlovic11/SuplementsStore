using System.Collections.Generic;

namespace SuplementsStore1.Models
{
    //klasa sluzi dok se ne dada prava baza podataka
    public class FakeProductRepository/* : IProductRepository*/
    {
        public IEnumerable<Product> Products => new List<Product>
        {
            new Product {Name="Protein", Price=80M},
            new Product {Name="Creatine", Price=15M},
            new Product {Name="Vitamin C", Price=16M}
        };
    }
}
