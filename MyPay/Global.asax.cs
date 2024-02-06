using MyPay.Models.Common;
using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using log4net;
using System.Configuration;

namespace MyPay
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            log4net.Config.XmlConfigurator.Configure();
            AppContext.SetSwitch("Switch.System.Security.Cryptography.Xml.UseInsecureHashAlgorithms", true);
            AppContext.SetSwitch("Switch.System.Security.Cryptography.Pkcs.UseInsecureHashAlgorithms", true);
            MvcHandler.DisableMvcResponseHeader = true;
            if (Common.ApplicationEnvironment.IsDevelopmentMachine == false)
            {
                BundleTable.EnableOptimizations = true;
            }

            //loadVotingSetting(EnvironmentVariableTarget.Machine);

            //log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));
            //ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            //log.Info("Log4Net is working. VotingAPISetting.voting_BaseURL: " + VotingAPISetting.voting_BaseURL);

            //var plasmaTechEndPoint = Environment.GetEnvironmentVariable("PlasmaTech_EndPoint", EnvironmentVariableTarget.Machine);
            //var PlasmaTechUserName = Environment.GetEnvironmentVariable("PlasmaTech_UserName", EnvironmentVariableTarget.Machine);
            //var PlasmaTechPassword = Environment.GetEnvironmentVariable("PlasmaTech_Password", EnvironmentVariableTarget.Machine);
            //var PlasmaTechAgencyId = Environment.GetEnvironmentVariable("PlasmaTech_AgencyId", EnvironmentVariableTarget.Machine);
        }

        //private void loadVotingSetting(EnvironmentVariableTarget target)
        //{
        //    VotingAPISetting.voting_BaseURL = Environment.GetEnvironmentVariable("Voting_BaseURL", target);
        //    VotingAPISetting.key = Environment.GetEnvironmentVariable("Voting_Key", target);
        //    VotingAPISetting.user = Environment.GetEnvironmentVariable("Voting_User", target);
        //    VotingAPISetting.pass = Environment.GetEnvironmentVariable("Voting_Pass", target);
        //}

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

            //if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            //{
            //    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "*");
            //    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "*");
            //    HttpContext.Current.Response.End();
            //}

            //if (System.Configuration.ConfigurationManager.AppSettings["IsProduction"] == "0") {
            //    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", System.Configuration.ConfigurationManager.AppSettings["Origins"]);

            //    if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            //    {
            //        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", System.Configuration.ConfigurationManager.AppSettings["Methods"]);
            //        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", System.Configuration.ConfigurationManager.AppSettings["Headers"]);
            //        HttpContext.Current.Response.End();
            //    }
            //}

            //string origins = System.Configuration.ConfigurationManager.AppSettings["Origins"];
            //string [] ListOfOrigins = origins.Split(',');

            //foreach (string str in ListOfOrigins)
            //{
            //    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin",str);
            //}


            // HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "https://customer.mypay.com.np");
              // HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", System.Configuration.ConfigurationManager.AppSettings["Origins"]);


            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", System.Configuration.ConfigurationManager.AppSettings["Methods"]);
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", System.Configuration.ConfigurationManager.AppSettings["Headers"]);

                //HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
                HttpContext.Current.Response.End();
            }


            //if (System.Configuration.ConfigurationManager.AppSettings["IsProduction"] == "0")
            //{
            //    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

            //    if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            //    {
            //        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
            //        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "UToken,secret,API_KEY,secret-key");
            //        HttpContext.Current.Response.End();
            //    }
            //}



            // Create HttpApplication and HttpContext objects to access
            // request and response properties.
            //HttpApplication application = (HttpApplication)sender;
            //HttpContext context = application.Context;
            //string filePath = context.Request.FilePath;
            //string fileExtension =
            //    VirtualPathUtility.GetExtension(filePath);
            //if (fileExtension.Equals(".css") || fileExtension.Equals(".js") || fileExtension.Equals(".jpeg") || fileExtension.Equals(".jpg") || fileExtension.Equals(".png"))
            //{
            //    context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            //}
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null || authCookie.Value == "")
            {
                return;
            }
            FormsAuthenticationTicket authTicket;
            try
            {
                authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            }
            catch
            {
                return;
            }
            // retrieve roles from UserData
            string[] roles = authTicket.UserData.Split(';');
            if (Context.User != null)
            {
                Context.User = new GenericPrincipal(Context.User.Identity, null);
            }
        }
    }
}
