using Microsoft.AspNetCore.Mvc;

namespace TicketSys.WebUI.Controllers
{
    public class OrderServiceController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

    }
}
