using Microsoft.AspNetCore.Mvc;
using SuplementsStore1.Models;
using System.Linq;

namespace SuplementsStore1.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository repository;
        private readonly Cart cart;

        public OrderController(IOrderRepository repository, Cart cart)
        {
            this.repository = repository;
            this.cart = cart;
        }

        //metoda koja se koristi da bi se prikazala lista neposlatih
        //narudzbina adminisratoru  
        public ViewResult List() 
        {
            return View(repository.Orders.Where(o => !o.Shipped));
        }

        //ova metoda ce primiti ID narudzbine da bi locirao
        //ordedjeni order u bazi i postavio njegov Shipped properti na true i sacuvao
        [HttpPost]
        public IActionResult MarkAsShipped(int orderID)
        {
            Order order = repository.Orders
                .FirstOrDefault(o => o.OrderID == orderID); 
            if (order != null)
            {
                order.Shipped = true;
                repository.SaveOrder(order);
            }
            return RedirectToAction("List");
        }
        //checkout
        public IActionResult Checkout()
        {
            return View(new Order());
        }
        //slanje podataka o orderu administratoru
        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("Name", "Sorry, your cart is empty!");
                TempData["message"] = "Sorry, your cart is empty!";
            }
            if (ModelState.IsValid)
            {
                order.Lines = cart.Lines.ToArray();
                repository.SaveOrder(order);
                return RedirectToAction("CompletedOrder");
            }
            else
            {
                return View(order);
            }
        }
        //completed order view i brisanje korpe
        public ViewResult CompletedOrder()
        {
            cart.Clear();
            return View();
        }
    }
}
