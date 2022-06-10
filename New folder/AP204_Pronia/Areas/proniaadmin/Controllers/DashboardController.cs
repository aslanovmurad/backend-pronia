using Microsoft.AspNetCore.Mvc;

namespace AP204_Pronia.Areas.proniaadmin.Controllers
{
    public class DashboardController : Controller
    {
        [Area("proniaadmin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
