using Microsoft.AspNetCore.Mvc;

namespace PaymentApp.API.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
