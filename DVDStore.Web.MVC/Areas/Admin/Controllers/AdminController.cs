using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace DVDStore.Web.MVC.Areas.Admin.Controllers
{
    /// <summary>
    ///  Admin Controller 
    /// </summary> <summary>
    /// 
    /// </summary>
    [Area("Admin")]
    [Route("Admin/[controller]")]
    public class AdminController : Controller
    {
        /// <summary>
        ///  Index Action
        /// </summary>
        /// <returns></returns>
        [Route("Index")]
        public IActionResult Index()
        {
            Debug.WriteLine("Entered Index method of AdminController");
            return View();
        }
    }
}
