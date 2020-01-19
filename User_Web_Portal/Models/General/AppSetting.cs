using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace User_Web_Portal.Models.General
{
    public class AppSetting
    {
        public static string ConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["database"].ConnectionString;
        }
    }
}