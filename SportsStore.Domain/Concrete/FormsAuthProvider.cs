using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SportsStore.Domain.Abstract;
using System.Web.Security;


namespace SportsStore.Domain.Concrete
{

    public class FormsAuthProvider : IAutProvider
    {

        public bool Authenticate(string username, string password)
        {
            bool resutl = FormsAuthentication.Authenticate(username, password);
            if (resutl)
            {
                FormsAuthentication.SetAuthCookie(username, false);
            }
            return resutl;
        }
    }
}
