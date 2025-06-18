using Microsoft.AspNetCore.Mvc;

namespace PaymentApp.API.Controllers
{
    public class RefundController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
