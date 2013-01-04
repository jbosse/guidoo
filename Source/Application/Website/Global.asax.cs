using System;
using Website.App;

namespace Website
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            new GuidooAppHost().Init();
        }
    }
}