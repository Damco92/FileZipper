using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace FileArchiver.MVC.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Index()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            ViewBag.ExceptionPath = exceptionDetails.Path;
            ViewBag.ExceptionMessage = exceptionDetails.Error.Message;
            ViewBag.StackTrace = exceptionDetails.Error.StackTrace;
            return View("_ErrorPage");
        }
    }
}
