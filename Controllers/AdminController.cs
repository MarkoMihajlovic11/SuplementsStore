using Microsoft.AspNetCore.Mvc;
using SuplementsStore1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuplementsStore1.Controllers
{
    public class AdminController : Controller
    {
        private readonly IProductRepository repository;

        public AdminController(IProductRepository repository)
        {
            this.repository = repository;
        }
        //lista proizvoda u admin-viewu
        public IActionResult Index()
        {
            return View(repository.Products);
        }
        //izmena prozivoda
        public ViewResult Edit(int productId)
        {
            return View(repository.Products
                .FirstOrDefault(p => p.ProductID == productId));
        }

        //ukoliko je validacija bila uspesna, korisnik se vraca da vidi listu izmenjenih-dodatih proizvoda
        //ukoliko nije vraca se na Edit/AddNew stranicu 
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["message"] = $"{product.Name} has been saved"; //succes message
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }
        //kreiranje novih porizvoda
        //vraca edit viev i dodaje proizvod, da ne bi doslo do dupliranja pogleda
        public ViewResult Create()
        {
            return View("Edit", new Product());
        }
        //brisanje i succes message 
        [HttpPost] 
        public IActionResult Delete(int productId)
        { 
            Product deletedProduct = repository.DeleteProduct(productId);
            if (deletedProduct != null)
            { 
                TempData["message"] = $"{deletedProduct.Name} was deleted";
            } 
            return RedirectToAction("Index"); 
        }
    }
}
 