﻿using System;
using System.Web;
using System.Web.Mvc;
using W1.Domain.Entities;

namespace W1.WebUI.Binders
{
    public class CartModelBinder : IModelBinder
    {
        private const string sessionKey = "Cart";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) 
        { 
            // get the Cart from the session 
            Cart cart = (Cart)controllerContext.HttpContext.Session[sessionKey];  
            // create the Cart if there wasn't one in the session data 
            if (cart == null) 
            { 
                cart = new Cart(); 
                controllerContext.HttpContext.Session[sessionKey] = cart; 
            } 
 
            // return the cart 
            return cart; 
        }
    } 
}