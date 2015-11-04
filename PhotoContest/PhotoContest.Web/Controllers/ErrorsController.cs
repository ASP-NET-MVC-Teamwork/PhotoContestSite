namespace PhotoContest.Web.Controllers
{
    using System.Web.Mvc;

    public class ErrorsController : Controller
    {
        // GET: 404
        [ActionName("404")]
        public ActionResult NotFound()
        {
            Response.TrySkipIisCustomErrors = true;
            Response.StatusCode = 404;
            return this.View("Error");
        }
    }
}