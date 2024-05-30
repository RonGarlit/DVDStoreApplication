using DVDStore.Web.MVC.Areas.FilmCatalog.Common;
using DVDStore.Web.MVC.Areas.FilmCatalog.Models;
using DVDStore.Web.MVC.Areas.FilmCatalog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DVDStore.Web.MVC.Areas.FilmCatalog.Controllers
{
    [Area("FilmCatalog")]
    [Route("FilmCatalog/[controller]/[action]")]
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: Films/Create
        [HttpPost("{id?}")]
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
        [HttpPost("{id?}"), ActionName("Delete")]
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
        public async Task<IActionResult> Index([FromQuery] int pageNo = 1,
                                               [FromQuery] int pageSize = 10,
                                               [FromQuery] string? SearchQuery = null)
        {
            _logger.LogInformation("Films Catalog Index Page Entered");
            // Pass Search Query parameter forward for "Back To List" tag helper
            var resourceParameters = new FilmCatalogResourceParameters
            {
                PageNumber = pageNo,
                PageSize = pageSize,
                SearchQuery = SearchQuery
            };

            // Await the task to get the model
            var model = await _filmRepository.GetPagedFilms(resourceParameters);

            // Prep some needed ViewBag Variables
            ViewBag.SearchQuery = "";

            // Load up the ViewBag variables with data from the filters and search boxes
            if (resourceParameters.SearchQuery != null)
            {
                ViewBag.SearchQuery = resourceParameters.SearchQuery;
            }

            // Save the search query parameter forward so that is can be added to the "Back To List"
            BTLSearchQuery = ViewBag.SearchQuery;

            return View(model);
        }

        #endregion Public Methods
    }
}