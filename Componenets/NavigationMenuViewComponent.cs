using Microsoft.AspNetCore.Mvc;
using SuplementsStore1.Models;
using System.Linq;

namespace SuplementsStore1.Componenets
{
    //view component predstavlja klasu koja sadrzi logiku aplikacije 
    //koja se moze iskoristiti na vise mesta uz pomoc razor pogleda
    public class NavigationMenuViewComponent : ViewComponent
    {
        private readonly IProductRepository repository;

        public NavigationMenuViewComponent(IProductRepository repository)
        {
            this.repository = repository;
        }

        //Metoda Invoke se poziva kada se view component iskoristi u okviru Razor pogleda, 
        //rezultat koji ova metoda vratca se ugrađuje u okviru HTML-a koji se šalje browser-u.
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"]; //dodavanje vizualnog prikaza odabrane kateogrije u Default.cshtml
            return View(repository.Products
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x));
        }
    }
}
