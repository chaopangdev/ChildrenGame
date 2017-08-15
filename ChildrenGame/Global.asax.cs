using log4net;
using log4net.Config;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ChildrenGame
{
    public class ChildrenGameApplication : System.Web.HttpApplication
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(ChildrenGameApplication).Name);

        protected void Application_Start()
        {
            XmlConfigurator.Configure();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            _logger.Info("Application Started.");
        }

        protected void Application_Error()
        {
            var ex = Server.GetLastError();
            _logger.Error(ex);

            HttpContext httpContext = HttpContext.Current;
            if (httpContext != null)
            {
                RequestContext requestContext = ((MvcHandler)httpContext.CurrentHandler).RequestContext;

                if (requestContext.HttpContext.Request.IsAjaxRequest())
                {
                    httpContext.Response.Clear();
                    string controllerName = requestContext.RouteData.GetRequiredString("controller");
                    IControllerFactory factory = ControllerBuilder.Current.GetControllerFactory();
                    IController controller = factory.CreateController(requestContext, controllerName);
                    ControllerContext controllerContext = new ControllerContext(requestContext, (ControllerBase)controller);

                    JsonResult jsonResult = new JsonResult();
                    jsonResult.Data = new { success = false, serverError = "500" };
                    jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                    jsonResult.ExecuteResult(controllerContext);
                    httpContext.Response.End();
                }
                else
                {
                    httpContext.Response.Redirect("~/Error");
                }
            }
        }
    }
}
