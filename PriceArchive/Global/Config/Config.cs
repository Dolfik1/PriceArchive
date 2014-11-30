using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace PriceArchive.Global.Config
{
    public class Config : IConfig
    {
        public string Lang
        {
            get
            {
                return ConfigurationManager.AppSettings["Culture"] as string;
            }
        }
    }
}