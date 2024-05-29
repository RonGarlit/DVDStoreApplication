using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DVDStore.Web.MVC.Areas.FilmCatalog.Repositories;
using DVDStore.Web.MVC.Areas.FilmCatalog.Common;
using DVDStore.Web.MVC.Areas.FilmCatalog.Models;

namespace DVDStore.Web.MVC.Areas.FilmCatalog.Controllers
{
    public class FilmsCatalogController : Controller
    {
        private readonly FilmRepository _filmRepository;

        public FilmsCatalogController(FilmRepository filmRepository)
        {
            _filmRepository = filmRepository;
        }

        // GET: Films
        public async Task<IActionResult> Index(FilmCatalogResourceParameters resourceParameters)
        {
            var model = await _filmRepository.GetPagedFilms(resourceParameters);
            return View(model);
        }

        // GET: Films/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var model = await _filmRepository.GetFilm(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // GET: Films/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Films/Create
        [HttpPost]
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

        // GET: Films/Edit/5
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
        [HttpPost]
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


        // GET: Films/Delete/5
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
        [HttpPost, ActionName("Delete")]
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
    }
}
