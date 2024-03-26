using Microsoft.AspNetCore.Mvc;

namespace DVDStore.Web.MVC.Areas.Admin.Controllers
{
    /// <summary>
    ///  Admin Controller 
    /// </summary> <summary>
    /// 
    /// </summary>
    [Area("Admin")]
    public class AdminController : Controller
    {
        /// <summary>
        ///  Index Action
        /// </summary>
        /// <returns></returns>
        [Route("Admin/Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
