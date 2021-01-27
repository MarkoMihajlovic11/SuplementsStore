using Microsoft.AspNetCore.Mvc;
using SuplementsStore1.Infrastructure;
using SuplementsStore1.Models;
using SuplementsStore1.Models.ViewModels;
using System.Linq;

namespace SuplementsStore1.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepository repository;
        private Cart cart;

        public CartController(IProductRepository repository, Cart cartService)
        {
            this.repository = repository;
            cart = cartService;

        }
        //metoda dobija Cart objekat iz sesije i koristi ga kako bi kreirala 
        //CartIndexViewModel objekat, koji se zatim prosleđuje ka View metodi 
        public ViewResult Index(string returnUlr)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUlr

            });

        }
        //dodavanje u korpu
        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        //brisanje iz korpe
        public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
    }
}