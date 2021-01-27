using Microsoft.AspNetCore.Mvc;
using SuplementsStore1.Models;
using SuplementsStore1.Models.ViewModels;
using System.Linq;

namespace SuplementsStore1.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository repository;
        public int PageSize = 4;  //koliko prizvoda zelimo po starnici

        public ProductController(IProductRepository repository)
        {
            this.repository = repository;
        }
        //vraca listu proizvoda kroz view
        public ViewResult List(string category, int page = 1) //ukoliko ne unesemo parametar u url default je 1
        {
            return View(new ProductsListViewModel
            {
                Products = repository.Products
                    .Where(p => category == null || p.Category == category) //katogrija je jednaka izbranoj ili nijednoj
                    .OrderBy(p => p.Name)  //sortiranje prozivoda po imenu
                    .Skip((page - 1) * PageSize) //ignorisanje onih koji nisu na stranici
                    .Take(PageSize), //uzimanje 4 po sstranici
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ?
                        repository.Products.Count() : 
                        repository.Products.Where(e => 
                            e.Category == category).Count() //paginacija sa ukljucenom kateogrijom
                },
                CurrentCategory = category
            }); 
        }

        //vraca rezultat pretrage, ukoliko nije uneta vraca pocetnu stranicu
        public ViewResult ViewSearchResult(string SearchTerm)
        {
            if (SearchTerm != null)
            {
                return View(repository.Products.Where(p => p.Name.ToLower().Contains(SearchTerm.ToLower())
                 || p.Category.ToLower().Contains(SearchTerm.ToLower())
                 || p.Description.ToLower().Contains(SearchTerm.ToLower())));
            }
            else
            {
                return View(repository.Products);
            }    
        }
    }
}