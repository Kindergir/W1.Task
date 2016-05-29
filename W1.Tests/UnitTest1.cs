using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using W1.Domain.Abstract;
using W1.Domain.Entities;
using W1.WebUI.Controllers;
using W1.WebUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace W1.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate() 
    { 
      // ...statements removed for brevity... 
    }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            // Arrange - define an HTML helper - we need to do this 
            // in order to apply the extension method 
            HtmlHelper myHelper = null;

            // Arrange - create PagingInfo data 
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            // Arrange - set up the delegate using a lambda expression 
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            // Act 
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            // Assert 
            Assert.AreEqual(result.ToString(), @"<a href=""Page1"">1</a>"
              + @"<a class=""selected"" href=""Page2"">2</a>"
              + @"<a href=""Page3"">3</a>");
        }
    }
}