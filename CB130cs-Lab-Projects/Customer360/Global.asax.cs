using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace Customer360
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            CouchbaseConfig.Setup();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        protected void Application_End()
        {
            CouchbaseConfig.Cleanup();
        }
    }
}
