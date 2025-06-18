using Microsoft.AspNetCore.Mvc;

namespace PaymentApp.API.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
