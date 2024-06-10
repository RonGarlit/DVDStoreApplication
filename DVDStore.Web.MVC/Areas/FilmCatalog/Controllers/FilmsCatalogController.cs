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
**  FileName: FilmsCatalogController.cs (DVDStore Application)
**  Version: 0.1
**  Author: Ronald Garlit
**
**  Description: This file contains the FilmsCatalogController class for the DVDStore
**  web application.
**
**  The FilmsCatalogController class handles actions related to the film catalog, such
**  as listing, creating, editing, and deleting films.
**
**  Change History
**
**  WHEN			WHO        WHAT
**---------------------------------------------------------------------------------
**  2024-05-28		RGARLIT     STARTED DEVELOPMENT
***********************************************************************************/

using DVDStore.Web.MVC.Areas.FilmCatalog.Common;
using DVDStore.Web.MVC.Areas.FilmCatalog.Models;
using DVDStore.Web.MVC.Areas.FilmCatalog.Repositories;
using DVDStore.Web.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DVDStore.Web.MVC.Areas.FilmCatalog.Controllers
{
    /// <summary>
    ///  FilmsCatalogController class handles actions related to the film
    ///  catalog, such as listing, creating, editing, and deleting films.
    /// </summary>
    [Area("FilmCatalog")]
    [Route("FilmCatalog/[controller]/[action]")]
    [Authorize(Roles = "Manager")]
    public class FilmsCatalogController : Controller
    {
        #region Private Fields

        // _filmRepository is the Film Repository
        private readonly IFilmRepository _filmRepository;

        // _logger is the Logger
        private readonly ILogger<FilmsCatalogController> _logger;

        #endregion Private Fields

        #region Private Properties

        /// <summary>
        /// BTLSearchQuery is the Back To List Search Query
        /// </summary>
        private static string? BTLSearchQuery { get; set; }

        #endregion Private Properties

        #region Public Constructors

        /// <summary>
        /// FilmsCatalogController constructor initializes the Film Repository and Logger
        /// </summary>
        /// <param name="filmRepository"></param>
        /// <param name="logger"></param>
        public FilmsCatalogController(IFilmRepository filmRepository, ILogger<FilmsCatalogController> logger)
        {
            _filmRepository = filmRepository;
            _logger = logger;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Create method returns the Create Film page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Manager")]
        public IActionResult Create()
        {
            // Try to open the Create Film page
            try
            {
                // Open the Create Film page
                return View();
            }
            // Catch any exceptions
            catch (Exception ex)
            {
                // Log the error
                _logger.LogError(ex, "Error while accessing the Create Film page");
                // Return the Error page
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        /// <summary>
        /// Create method creates a new film
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Manager")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FilmViewModel model)
        {
            // Try to create a new film
            try
            {
                // Check if the model is valid
                if (ModelState.IsValid)
                {
                    // Create a new film in the database through the repository
                    await _filmRepository.AddFilm(model);
                    // Redirect to the Films Catalog Index page
                    return RedirectToAction(nameof(Index));
                }
                // Return the Create Film page with the model because it is not valid
                return View(model);
            }
            // Catch any exceptions
            catch (Exception ex)
            {
                // Log the error
                _logger.LogError(ex, "Error while creating a film");
                // Return the Error page with the error message
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        /// <summary>
        /// Delete method returns the Delete Film page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id?}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Delete(int id)
        {
            // Try to open the Delete Film page
            try
            {
                // Log the entry to the Delete Film page
                _logger.LogInformation("Films Catalog Delete Page Entered");
                // Get the film from the database via the repository
                var model = await _filmRepository.GetFilm(id);
                // Check if the film is not found
                if (model == null)
                {
                    // Log the error
                    _logger.LogWarning("Films Catalog Delete Page Entered - Film Not Found");
                    // Return the Not Found page
                    return NotFound();
                }
                // Pass Search Query parameter forward for "Back To List" tag helper
                ViewBag.IndexSearchQuery = BTLSearchQuery;
                // Return the Delete Film page with the model
                return View(model);
            }
            // Catch any exceptions
            catch (Exception ex)
            {
                // Log the error
                _logger.LogError(ex, "Error accessing Film Delete page");
                // Return the Error page with the error message
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        /// <summary>
        /// DeleteConfirmed method deletes the film
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        [Authorize(Roles = "Manager")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Try to delete the film
            try
            {
                // Delete the film from the database via the repository
                var success = await _filmRepository.DeleteFilm(id);
                // Check result of the deletion via the repository
                if (!success)
                {
                    // Requested film to delete was not found
                    return NotFound();
                }
                // Setup the Back To List Search Query value
                ViewBag.IndexSearchQuery = BTLSearchQuery;
                // Redirect to the Films Catalog Index page after successful deletion
                return RedirectToAction(nameof(Index));
            }
            // Catch any exceptions
            catch (Exception ex)
            {
                // Log the error
                _logger.LogError(ex, "Error while confirming film deletion");
                // Return the Error page with the error message
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        /// <summary>
        /// Details method returns the Film Details page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id?}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Details(int id)
        {
            // Pass Search Query parameter forward for "Back To List" tag helper
            ViewBag.IndexSearchQuery = BTLSearchQuery;
            // Try to open the Film Details page
            try
            {
                //Get the film from the database via the repository
                var model = await _filmRepository.GetFilm(id);
                // Check the return of the film model from the repository
                if (model == null)
                {
                    // return Not Found if the film is not found
                    return NotFound();
                }
                // Return the Film Details page with the model to the view
                return View(model);
            }
            // catch any exceptions
            catch (Exception ex)
            {
                // Log the error
                _logger.LogError(ex, "Error loading Film Details page for Film ID: {FilmId}", id);
                // Return the Error page with the error message
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        /// <summary>
        /// Edit method returns the Edit Film page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id?}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Edit(int id)
        {
            // Try to open the Edit Film page
            try
            {
                // Log the entry to the Edit Film page
                _logger.LogInformation("Attempting to retrieve film for edit. Film ID: {FilmId}", id);
                // Get the film from the database via the repository
                var model = await _filmRepository.GetFilm(id);
                // Check if the film is not found
                if (model == null)
                {
                    // Log the error
                    _logger.LogWarning("Edit attempted for non-existing film. Film ID: {FilmId}", id);
                    // Return the Not Found page
                    return NotFound();
                }
                // Pass Search Query parameter forward for "Back To List" tag helper
                ViewBag.IndexSearchQuery = BTLSearchQuery;
                // Return the Edit Film page with the model
                return View(model);
            }
            // Catch any exceptions
            catch (Exception ex)
            {
                // Log the error
                _logger.LogError(ex, "Error retrieving film for edit. Film ID: {FilmId}", id);
                // Return the Error page with the error message
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        /// <summary>
        /// Edit method updates the film
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Manager")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(FilmViewModel model)
        {
            // Try to update the film
            try
            {
                // Check if the model is valid
                if (ModelState.IsValid)
                {
                    // Log the update attempt
                    _logger.LogInformation("Attempting to update film. Film ID: {FilmId}", model.Filmid);
                    // Update the film in the database via the repository
                    var updatedModel = await _filmRepository.UpdateFilm(model);
                    // Check if the film is not found
                    if (updatedModel == null)
                    {
                        // Log the error
                        _logger.LogWarning("Failed to find the film for update. Film ID: {FilmId}", model.Filmid);
                        // Return Not Found if the film is not found
                        return NotFound();
                    }
                    // Setup the Back To List Search Query value
                    ViewBag.IndexSearchQuery = BTLSearchQuery;
                    // return to the Films Catalog Index page after successful update
                    return RedirectToAction(nameof(Index));
                }
                // Return the Edit Film page with the model because it is not valid
                return View(model);
            }
            // Catch any exceptions
            catch (Exception ex)
            {
                // Log the error
                _logger.LogError(ex, "Error updating film. Film ID: {FilmId}", model.Filmid);
                // Return the Error page with the error message
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        /// <summary>
        /// Index method returns the Films Catalog Index page, with optional search query
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <param name="SearchQuery"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Index([FromQuery] int pageNo = 1,
                                               [FromQuery] int pageSize = 10,
                                               [FromQuery] string? SearchQuery = null)
        {
            // Try to open the Films Catalog Index page
            try
            {
                // Log the entry to the Films Catalog Index page
                _logger.LogInformation("Films Catalog Index Page Entered");
                // during Debug, log the page number, page size, and search query
                _logger.LogDebug("Page Number: {PageNumber}, Page Size: {PageSize}, Search Query: {SearchQuery}", pageNo, pageSize, SearchQuery);
                // Set the Back To List Search Query value
                BTLSearchQuery = SearchQuery;
                // Create a new FilmCatalogResourceParameters object
                var resourceParameters = new FilmCatalogResourceParameters
                {
                    PageNumber = pageNo,
                    PageSize = pageSize,
                    SearchQuery = SearchQuery
                };
                // Get the paged films from the database via the repository
                var model = await _filmRepository.GetPagedFilms(resourceParameters);
                // Return the Films Catalog Index page with the model to the view
                return View(model);
            }
            // Catch any exceptions
            catch (Exception ex)
            {
                // Log the error
                _logger.LogError(ex, "Error loading Films Catalog Index page {ErrorMessage}", ex.Message);
                // Return the Error page with the error message
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        #endregion Public Methods
    }
}

/*
		 Lunar lander from the apollo missions
                 ____
                /___.`--.____ .--. ____.--(
                       .'_.- (    ) -._'.
                     .'.'    |'..'|    '.'.
              .-.  .' /'--.__|____|__.--'\ '.  .-.
             (O).)-| |  \    |    |    /  | |-(.(O)
              `-'  '-'-._'-./      \.-'_.-'-'  `-'
                 _ | |   '-.________.-'   | | _
              .' _ | |     |   __   |     | | _ '.
             / .' ''.|     | /    \ |     |.'' '. \
             | |( )| '.    ||      ||    .' |( )| |
             \ '._.'   '.  | \    / |  .'   '._.' /
              '.__ ______'.|__'--'__|.'______ __.'
             .'_.-|         |------|         |-._'.
            //\\  |         |--::--|         |  //\\
           //  \\ |         |--::--|         | //  \\
          //    \\|        /|--::--|\        |//    \\
         / '._.-'/|_______/ |--::--| \_______|\`-._.' \
        / __..--'        /__|--::--|__\        `--..__ \
       / /               '-.|--::--|.-'               \ \
      / /                   |--::--|                   \ \
     / /                    |--::--|                    \ \
 _.-'  `-._                 _..||.._                  _.-` '-._
'--..__..--' LGB           '-.____.-'                '--..__..-'

 */