﻿using System.Threading.Tasks;
using DVDStore.Web.MVC.Areas.Store.Common;
using DVDStore.Web.MVC.Areas.Store.Models;
using DVDStore.Web.MVC.Areas.Store.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DVDStore.Web.MVC.Areas.Store.Controllers
{
    [Area("Store")]
    [Route("Store/[controller]/[action]")]
    public class StoreController : Controller
    {
        private readonly IFilmlistRepository _filmlistRepository;
        private readonly ILogger<StoreController> _logger;

        public StoreController(IFilmlistRepository filmlistRepository, ILogger<StoreController> logger)
        {
            _filmlistRepository = filmlistRepository;
            _logger = logger;
        }

        // GET: Store
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] int pageNo = 1, [FromQuery] int pageSize = 10, [FromQuery] string? SearchQuery = null, [FromQuery] string? Category = null, [FromQuery] string? Rating = null)
        {
            _logger.LogInformation("Store Index Page Entered");

            var resourceParameters = new StoreResourceParameters
            {
                PageNumber = pageNo,
                PageSize = pageSize,
                SearchQuery = SearchQuery,
                Category = Category,
                Rating = Rating
            };

            var model = await _filmlistRepository.GetPagedFilmlists(resourceParameters);
            return View(model);
        }
    }
}