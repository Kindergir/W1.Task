using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using W1.Domain.Entities;
using W1.Domain.Abstract;
using W1.WebUI.Models;

namespace W1.WebUI.Controllers
{
    public class ProductController : Controller
    {
        public int pageSize = 4;

        public ViewResult List(string category, int page = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = DataBasesAPI.DataBaseExplorer.GetProductsFromDataBase(category, page, pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = DataBasesAPI.DataBaseExplorer.GetTotalCount(category)
                },
                CurrentCategory = category,
            };

            return View(model); 
        }

        public FileContentResult GetImage(int productId)
        {
            Product prod = DataBasesAPI.DataBaseExplorer.GetProductFromDataBase(productId);
            if (prod != null)
            {
                return File(prod.ImageData, prod.ImageMimeType);
            }
            else
            {
                return null;
            }
        } 
    }
}