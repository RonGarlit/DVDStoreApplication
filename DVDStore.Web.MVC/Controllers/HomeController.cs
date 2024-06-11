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

using DVDStore.Web.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DVDStore.Web.MVC.Controllers;

/// <summary>
/// HomeController class for the DVDStore web application.
/// </summary>
public class HomeController : Controller
{
    #region Private Fields

    // Logger for HomeController
    private readonly ILogger<HomeController> _logger;

    #endregion Private Fields

    #region Public Constructors

    /// <summary>
    /// HomeController constructor.
    /// </summary>
    /// <param name="logger"></param>
    public HomeController(ILogger<HomeController> logger)
    {
        // Initialize logger
        _logger = logger;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <summary>
    /// Error method for the HomeController.
    /// </summary>
    /// <returns></returns>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        // Log error entry
        Debug.WriteLine("Entered Error method of HomeController");
        _logger.LogInformation("Entered Error method of HomeController");
        // Return error view
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    /// <summary>
    /// Index method for the HomeController.
    /// </summary>
    /// <returns></returns>
    public IActionResult Index()
    {
        // Log index entry
        Debug.WriteLine("Entered Index method of HomeController");
        _logger.LogInformation("Entered Index method of HomeController");
        // Return index view
        return View();
    }

    /// <summary>
    /// Privacy method for the HomeController.
    /// </summary>
    /// <returns></returns>
    public IActionResult Privacy()
    {
        // Log privacy entry
        Debug.WriteLine("Entered Privacy method of HomeController");
        _logger.LogInformation("Entered Privacy method of HomeController");
        // Return privacy view
        return View();
    }

    #endregion Public Methods
}