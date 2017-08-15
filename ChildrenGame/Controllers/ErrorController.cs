using System.Web.Mvc;

namespace ChildrenGame.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View("Share/Error");
        }
    }
}