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
    [Area("FilmCatalog")]
    [Route("FilmCatalog/[controller]/[action]")]
    [Authorize(Roles = "Manager")]
    public class FilmsCatalogController : Controller
    {
        #region Private Fields

        private readonly IFilmRepository _filmRepository;
        private readonly ILogger<FilmsCatalogController> _logger;

        #endregion Private Fields

        #region Private Properties

        private static string? BTLSearchQuery { get; set; }

        #endregion Private Properties

        #region Public Constructors

        public FilmsCatalogController(IFilmRepository filmRepository, ILogger<FilmsCatalogController> logger)
        {
            _filmRepository = filmRepository;
            _logger = logger;
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public IActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while accessing the Create Film page");
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }


        // POST: Films/Create

        [HttpPost]
        [Authorize(Roles = "Manager")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FilmViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _filmRepository.AddFilm(model);
                    return RedirectToAction(nameof(Index));
                }
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating a film");
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }


        [HttpGet("{id?}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _logger.LogInformation("Films Catalog Delete Page Entered");
                var model = await _filmRepository.GetFilm(id);
                if (model == null)
                {
                    _logger.LogWarning("Films Catalog Delete Page Entered - Film Not Found");
                    return NotFound();
                }
                ViewBag.IndexSearchQuery = BTLSearchQuery;
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error accessing Film Delete page");
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }


        [HttpPost("{id}")]
        [Authorize(Roles = "Manager")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var success = await _filmRepository.DeleteFilm(id);
                if (!success)
                {
                    return NotFound();
                }
                ViewBag.IndexSearchQuery = BTLSearchQuery;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while confirming film deletion");
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }


        [HttpGet("{id?}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var model = await _filmRepository.GetFilm(id);
                if (model == null)
                {
                    return NotFound();
                }
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading Film Details page for Film ID: {FilmId}", id);
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }


        [HttpGet("{id?}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                _logger.LogInformation("Attempting to retrieve film for edit. Film ID: {FilmId}", id);
                var model = await _filmRepository.GetFilm(id);
                if (model == null)
                {
                    _logger.LogWarning("Edit attempted for non-existing film. Film ID: {FilmId}", id);
                    return NotFound();
                }
                // Pass Search Query parameter forward for "Back To List" tag helper
                ViewBag.IndexSearchQuery = BTLSearchQuery;
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving film for edit. Film ID: {FilmId}", id);
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }


        [HttpPost]
        [Authorize(Roles = "Manager")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(FilmViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("Attempting to update film. Film ID: {FilmId}", model.Filmid);
                    var updatedModel = await _filmRepository.UpdateFilm(model);
                    if (updatedModel == null)
                    {
                        _logger.LogWarning("Failed to find the film for update. Film ID: {FilmId}", model.Filmid);
                        return NotFound();
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating film. Film ID: {FilmId}", model.Filmid);
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }


        [HttpGet]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Index([FromQuery] int pageNo = 1,
                                               [FromQuery] int pageSize = 10,
                                               [FromQuery] string? SearchQuery = null)
        {
            try
            {
                _logger.LogInformation("Films Catalog Index Page Entered");
                var resourceParameters = new FilmCatalogResourceParameters
                {
                    PageNumber = pageNo,
                    PageSize = pageSize,
                    SearchQuery = SearchQuery
                };

                var model = await _filmRepository.GetPagedFilms(resourceParameters);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading Films Catalog Index page {ErrorMessage}", ex.Message);
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }


        #endregion Public Methods
    }
}