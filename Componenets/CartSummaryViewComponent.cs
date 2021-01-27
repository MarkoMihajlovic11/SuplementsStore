using Microsoft.AspNetCore.Mvc;
using SuplementsStore1.Models;

namespace SuplementsStore1.Componenets
{
    //zbog dodavanja sumarnog prikaza korpe
    public class CartSummaryViewComponent : ViewComponent
    {
        private readonly Cart cart;
        //constructor
        public CartSummaryViewComponent(Cart cart)
        {
            this.cart = cart;
        }
        //method Invoke
        public IViewComponentResult Invoke()
        {
            return View(cart);
        }
    }
}
