using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using W1.WebUI.Infrastructure.Abstract;
using System.Web.Security;

namespace W1.WebUI.Infrastructure.Concrete
{
    public class FormsAuthProvider : IAuthProvider
    {
        public bool Authenticate(string username, string password)
        {
            bool result = username == "admin" && password == "secret";

            if (result)
            {
                FormsAuthentication.SetAuthCookie(username, false);
            }

            return result;
        }
    }
}