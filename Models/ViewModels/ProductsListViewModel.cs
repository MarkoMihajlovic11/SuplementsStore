using System.Collections.Generic;

namespace SuplementsStore1.Models.ViewModels
{
    //svi podatci koji se salju od kontrolera ka pogledu su u ovoj klasi
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}
