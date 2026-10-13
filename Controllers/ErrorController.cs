using Microsoft.AspNetCore.Mvc;

namespace Sistema_IAM.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/NotFound")]
        public IActionResult NotFoundPage()
        {
            return View("NotFound");
        }
    }
}