using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using W1.Domain.Abstract;

namespace W1.WebUI.Controllers
{
    public class NavController : Controller
    {
        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;
            IDictionary<Int32, String> categories = DataBasesAPI.DataBaseExplorer.GetCategories();
            return PartialView(categories); 
        } 
    }
}