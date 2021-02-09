using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SuplementsStore1.Models;
using SuplementsStore1.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SuplementsStore1.Controllers
{
    public class AdminController : Controller
    {
        private readonly IProductRepository repository;
        private readonly IWebHostEnvironment hostingEnvironment;

        public AdminController(IProductRepository repository,
                               IWebHostEnvironment hostingEnvironment)
        {
            this.repository = repository;
            this.hostingEnvironment = hostingEnvironment;
        }

        //lista proizvoda u admin-viewu
        public IActionResult Index()
        {
            return View(repository.Products);
        }

        //izmena prozivoda view
        public ViewResult Edit(int productId)
        {
            Product product = repository.GetProduct(productId);
            ProductEditViewModel createOrEditProductViewModel = new ProductEditViewModel
            {
                ProductID = product.ProductID,
                Name = product.Name,
                Category = product.Category,
                Description = product.Description,
                Price=product.Price,
                ExistingPhotoPath = product.PhotoPath
            };

            return View(createOrEditProductViewModel);            
        }

        //ukoliko je validacija bila uspesna, korisnik se vraca da vidi listu izmenjenih-dodatih proizvoda
        //ukoliko nije vraca se na Edit/AddNew stranicu 
        [HttpPost]
        public IActionResult Edit(ProductEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Product product = repository.GetProduct(model.ProductID);
                product.Name = model.Name;
                product.Description = model.Description;
                product.Price = model.Price;
                product.Category = model.Category;

                if (model.Photo != null)
                {
                    if(model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath,
                            "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }

                    product.PhotoPath = ProcesUploadedPhoto(model);
                }
       
                repository.SaveProduct(product);
                TempData["message"] = $"{product.Name} has been updated"; //succes message
                return RedirectToAction("Index");
            }

            return View();
        }

        //create view
        public ViewResult Create()
        {
            return View();
        }

        //create view on post
        [HttpPost]
        public IActionResult Create(ProductCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqeFileName = ProcesUploadedPhoto(model);

                Product newProduct = new Product
                {
                    ProductID = 0,
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Category = model.Category,
                    PhotoPath = uniqeFileName
                };

                repository.SaveProduct(newProduct);
                TempData["message"] = $"{newProduct.Name} has been saved"; //succes message
                return RedirectToAction("Index");
            }

            return View(); 
        }

        //method za dobijanje uniqe file imena i cuvanje slike u projekat
        private string ProcesUploadedPhoto(ProductCreateViewModel model)
        {
            string uniqeFileName = null;
            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images"); // /~images
                uniqeFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName; //dobiti uniqe file name
                string filePath = Path.Combine(uploadsFolder, uniqeFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }//cuvanje slike u fajl path
            }

            return uniqeFileName;
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
 