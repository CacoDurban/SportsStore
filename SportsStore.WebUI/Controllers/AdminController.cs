using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;

namespace SportsStore.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IProductRepository repository;

        public AdminController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Products);
        }

        public ViewResult Edit(int productId)
        {
          

            Product product = repository.Products.FirstOrDefault(x => x.ProductID == productId);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
          

            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["message"] = string.Format("{0} has been saved", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }


        // difenrencias entre ViewBag, y TempData
        // ViewBag sirve para enviar datos entre controlador y vista, almacenas valores unicos para la sesion del usuario y no permite mantener los valores entre diferentes llamadas httpsRequets
        // TempData sirve para enviar datos entre controladaor y vista, almacenas valores unicos para la sesion del usuario y se elimina cuando termina la peticion HttpRequest


        public ViewResult Create()
        {
            return View("Edit", new Product());
        }

        [HttpPost]
        public ActionResult Delete(int productId)
        {
            var product = repository.Products.FirstOrDefault(x => x.ProductID == productId);
            if (product != null)
            {
                repository.DeleteProduct(product);
                TempData["message"] = string.Format("{0} was deleted", product.Name);
            }
             

            return RedirectToAction("Index");
        }

        public void SumarConsulta(int idProduct)
        {
            string f = "dsa";
        }
    }
}
