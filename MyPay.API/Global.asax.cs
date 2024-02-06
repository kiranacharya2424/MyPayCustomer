using MyPay.API.Models.Request.Voting.Partner;
using MyPay.Models.Common;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.IO;
using Newtonsoft.Json.Linq;
using System;
using System.Web;
using System.Linq;
using log4net;

namespace MyPay.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            log4net.Config.XmlConfigurator.Configure();
          
            loadVotingSetting(EnvironmentVariableTarget.Machine);

            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 30000;
            timer.Elapsed += timer_Elapsed;
            timer.Start();
        }
        private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Task<int> UpdateDataRunningTask = Common.UpdateDataRunningOperationAsync();
        }

        private void loadVotingSetting(EnvironmentVariableTarget target)
        {
            VotingAPISetting_API.voting_BaseURL = Environment.GetEnvironmentVariable("Voting_BaseURL", target);
            VotingAPISetting_API.key = Environment.GetEnvironmentVariable("Voting_Key", target);
            VotingAPISetting_API.user = Environment.GetEnvironmentVariable("Voting_User", target);
            VotingAPISetting_API.pass = Environment.GetEnvironmentVariable("Voting_Pass", target);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

            //if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            //{
            //    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "*");
            //    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "*");
            //    HttpContext.Current.Response.End();
            //}

            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));
            ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Methods: " + System.Configuration.ConfigurationManager.AppSettings["Methods"]);
            log.Info("Origins: " + System.Configuration.ConfigurationManager.AppSettings["Origins"]);
            log.Info("Headers: " + System.Configuration.ConfigurationManager.AppSettings["Headers"]);


            //string origins = System.Configuration.ConfigurationManager.AppSettings["Origins"];
            //string[] ListOfOrigins = origins.Split(',');

            //foreach (string str in ListOfOrigins)
            //{
            //    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", str);
            //}
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", System.Configuration.ConfigurationManager.AppSettings["Origins"]);

                if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
                {
                    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", System.Configuration.ConfigurationManager.AppSettings["Methods"]);
                    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", System.Configuration.ConfigurationManager.AppSettings["Headers"]);
                    HttpContext.Current.Response.End();
                }

                //if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
                //{
                //    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
                //    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "UToken,secret,API_KEY,secret-key");
                //    HttpContext.Current.Response.End();
                //}
            


            ////New code
            //if (Request.Headers.AllKeys.Contains("Origin") && Request.HttpMethod == "OPTIONS")
            //{
            //    Context.Response.AddHeader("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Key, Accept,Authorization,serverName, API_KEY");
            //    Context.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
            //    Context.Response.End();
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
    }
}
