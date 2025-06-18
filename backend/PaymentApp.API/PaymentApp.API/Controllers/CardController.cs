using Microsoft.AspNetCore.Mvc;

namespace PaymentApp.API.Controllers
{
    public class CardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
