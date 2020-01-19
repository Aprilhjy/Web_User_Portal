using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using WebMatrix.WebData;

namespace User_Web_Portal
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            InitializeAuthenticationProcess();
        }

        private void InitializeAuthenticationProcess()
        {
            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("database", "AdvisorProfile", "JosariID", "AdvisorName", true);
                //WebSecurity.CreateUserAndAccount("admin", "admin28");
                //Roles.CreateRole("Administrator");
                //Roles.CreateRole("Advisor");
                //Roles.AddUserToRole("admin", "Administrator");
            }
        }
    }
}
