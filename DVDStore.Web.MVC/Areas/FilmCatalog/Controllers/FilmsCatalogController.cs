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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        // GET: Films/Create
        [HttpGet("{id?}")]
        [Authorize(Roles = "Manager")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Films/Create

        [HttpPost("{id?}")]
        [Authorize(Roles = "Manager")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FilmViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _filmRepository.AddFilm(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Films/Delete/5
        [HttpGet("{id?}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Films Catalog Delete Page Entered");
            var model = await _filmRepository.GetFilm(id);
            if (model == null)
            {
                _logger.LogWarning("Films Catalog Delete Page Entered - Film Not Found");
                return NotFound();
            }
            // Pass Search Query parameter forward for "Back To List" tag helper
            ViewBag.IndexSearchQuery = BTLSearchQuery;
            return View(model);
        }

        // POST: Films/Delete/5
        [HttpPost("{id}")]
        [Authorize(Roles = "Manager")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _filmRepository.DeleteFilm(id);
            if (!success)
            {
                return NotFound();
            }
            // Pass Search Query parameter forward for "Back To List" tag helper
            ViewBag.IndexSearchQuery = BTLSearchQuery;
            return RedirectToAction(nameof(Index));
        }

        // GET: Films/Details/5
        [HttpGet("{id?}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Details(int id)
        {
            var model = await _filmRepository.GetFilm(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // GET: Films/Edit/5
        [HttpGet("{id?}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _filmRepository.GetFilm(id);
            if (model == null)
            {
                return NotFound();
            }
            // Pass Search Query parameter forward for "Back To List" tag helper
            ViewBag.IndexSearchQuery = BTLSearchQuery;
            return View(model);
        }

        // POST: Films/Edit/5
        [HttpPost("{id?}")]
        [Authorize(Roles = "Manager")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(FilmViewModel model)
        {
            if (ModelState.IsValid)
            {
                var updatedModel = await _filmRepository.UpdateFilm(model);
                if (updatedModel == null)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Films
        [HttpGet]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Index([FromQuery] int pageNo = 1,
                                               [FromQuery] int pageSize = 10,
                                               [FromQuery] string? SearchQuery = null)
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

        #endregion Public Methods
    }
}