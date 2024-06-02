
/**********************************************************************************
**
**  DVDStore Application v1.0
**
**  Copyright 2024 
**  Developed by:
**     Ronald Garlit.
**
**  This software was created for educational purposes.
**
**  Use is subject to license terms.
***********************************************************************************
**
**  FileName: HomeController.cs (DVDStore Application)
**  Version: 0.1
**  Author: Ronald Garlit
**
**  Description: This file contains the HomeController class for the DVDStore web application.
**  
**  The HomeController handles the main actions of the home page, including the index, privacy, and error pages.
**
**  Change History
**
**  WHEN			WHO        WHAT
**---------------------------------------------------------------------------------
**  2024-06-01		RGARLIT     STARTED DEVELOPMENT 
***********************************************************************************/
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
        _logger.LogInformation("Entered Error method of HomeController");
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult Index()
    {
        Debug.WriteLine("Entered Index method of HomeController");
        _logger.LogInformation("Entered Index method of HomeController");
        return View();
    }
    public IActionResult Privacy()
    {
        Debug.WriteLine("Entered Privacy method of HomeController");
        _logger.LogInformation("Entered Privacy method of HomeController");
        return View();
    }

    #endregion Public Methods
}
