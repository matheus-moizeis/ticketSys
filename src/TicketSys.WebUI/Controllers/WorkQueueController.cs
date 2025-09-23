using Microsoft.AspNetCore.Mvc;

namespace TicketSys.WebUI.Controllers
{
    public class WorkQueueController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

    }
}
