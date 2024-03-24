using Microsoft.AspNetCore.Mvc;

namespace DVDStore.Web.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        [Route("Admin/Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
