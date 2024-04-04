using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DVDStore.Web.MVC.Models;

namespace DVDStore.Web.MVC.Controllers;
public class HomeController : Controller
{
    #region Private Fields

    private readonly ILogger<HomeController> _logger;

    #endregion Private Fields

    #region Public Constructors

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
        
    }

    #endregion Public Constructors

    #region Public Methods

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        Debug.WriteLine("Entered Error method of HomeController");
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult Index()
    {
        Debug.WriteLine("Entered Index method of HomeController");
        return View();
    }
    public IActionResult Privacy()
    {
        Debug.WriteLine("Entered Privacy method of HomeController");
        return View();
    }

    #endregion Public Methods
}
