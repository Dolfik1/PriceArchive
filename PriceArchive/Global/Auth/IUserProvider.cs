using PriceArchive.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PriceArchive.Global.Auth
{
    public interface IUserProvider
    {
        User User { get; set; }
    }
}