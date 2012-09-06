using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsStore.Domain.Abstract
{
    public interface IAutProvider
    {
        bool Authenticate(string username, string password);
    }
}
