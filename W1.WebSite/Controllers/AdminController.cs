using System;
using System.Web.Mvc;
using System.Linq;
using W1.Domain.Abstract;
using W1.Domain.Entities;
using System.Web;
using System.IO;

namespace W1.WebUI.Controllers
{
    [Authorize] 
    public class AdminController : Controller
    {
        public ViewResult Index()
        {
            return View(DataBasesAPI.DataBaseExplorer.GetProductsFromDataBase(null, 1, DataBasesAPI.DataBaseExplorer.GetTotalCount()));
        }

        public ViewResult Edit(int productId)
        {
            Product product = DataBasesAPI.DataBaseExplorer.GetProductFromDataBase(productId);
            ViewData["Categories"] = new SelectList(DataBasesAPI.DataBaseExplorer.GetCategories(), "Key", "Value", product.CategoryID);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                DataBasesAPI.DataBaseExplorer.InsertProduct(product.ProductID == 0, product, image);
                TempData["message"] = string.Format("{0} has been saved", product.Name);
                return RedirectToAction("Index");
            }
            else
                return View(product);
        }

        public ViewResult AddCategory()
        {
            return View("AddCategory", String.Empty);
        }

        [HttpPost]
        public ActionResult AddCategory(String name)
        {
            DataBasesAPI.DataBaseExplorer.AddCategory(name);
            return RedirectToAction("Index");
        }

        public ViewResult Create()
        {
            return View("Edit", new Product());
        }

        [HttpPost]
        public ActionResult Delete(int productId)
        {
            DataBasesAPI.DataBaseExplorer.DeleteProduct(productId);
            return RedirectToAction("Index");
        }
    }
}