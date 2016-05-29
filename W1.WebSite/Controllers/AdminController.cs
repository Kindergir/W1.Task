﻿using System;
using System.Web.Mvc;
using System.Linq;
using W1.Domain.Abstract;
using W1.Domain.Entities;
using System.Web;

namespace W1.WebUI.Controllers
{
    [Authorize] 
    public class AdminController : Controller
    {
        private IProductRepository repository;

        public AdminController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(DataBasesAPI.DataBaseExplorer.GetProductsFromDataBase(null, 1, DataBasesAPI.DataBaseExplorer.GetTotalCount()));
        }

        public ViewResult Edit(int productId)
        {
            Product product = DataBasesAPI.DataBaseExplorer.GetProductFromDataBase(productId);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                 if (image != null)
                { 
                    product.ImageMimeType = image.ContentType; 
                    product.ImageData = new byte[image.ContentLength]; 
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength); 
                }

                DataBasesAPI.DataBaseExplorer.InsertProduct(product.ProductID == 0, product, image);
                TempData["message"] = string.Format("{0} has been saved", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                // there is something wrong with the data values 
                return View(product);
            }
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