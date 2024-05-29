using DVDStore.Web.MVC.Areas.FilmCatalog.Common;
using DVDStore.Web.MVC.Areas.FilmCatalog.Models;
using DVDStore.Web.MVC.Areas.FilmCatalog.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DVDStore.Web.MVC.Areas.FilmCatalog.Controllers
{
    [Area("FilmCatalog")]
    [Route("FilmCatalog/[controller]/[action]")]
    public class FilmsCatalogController : Controller
    {
        #region Private Fields

        private readonly FilmRepository _filmRepository;

        #endregion Private Fields

        #region Public Constructors

        public FilmsCatalogController(FilmRepository filmRepository)
        {
            _filmRepository = filmRepository;
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
            var model = await _filmRepository.GetFilm(id);
            if (model == null)
            {
                return NotFound();
            }
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
        public async Task<IActionResult> Index(FilmCatalogResourceParameters resourceParameters)
        {
            var model = await _filmRepository.GetPagedFilms(resourceParameters);
            return View(model);
        }

        #endregion Public Methods
    }
}