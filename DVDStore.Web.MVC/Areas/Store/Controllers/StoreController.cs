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
**  FileName: StoreController.cs (DVDStore Application)
**  Version: 0.1
**  Author: Ronald Garlit
**
**  Description: Store Controller for the Store Area of the DVDStore Application.
**
**  Change History
**
**  WHEN			WHO        WHAT
**---------------------------------------------------------------------------------
**  2024-05-31		RGARLIT     STARTED DEVELOPMENT
***********************************************************************************/

using DVDStore.Web.MVC.Areas.Store.Common;
using DVDStore.Web.MVC.Areas.Store.Repositories;
using DVDStore.Web.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DVDStore.Web.MVC.Areas.Store.Controllers
{
    /// <summary>
    /// Store Controller for the Store Area of the DVDStore Application.
    /// </summary>
    [Area("Store")]
    [Route("Store/[controller]/[action]")]
    public class StoreController : Controller
    {
        #region Private Fields
        // create a private field for the IFilmlistRepository and ILogger
        private readonly IFilmlistRepository _filmlistRepository;
        private readonly ILogger<StoreController> _logger;

        #endregion Private Fields

        #region Public Constructors
        /// <summary>
        /// Store Controller Constructor
        /// </summary>
        /// <param name="filmlistRepository"></param>
        /// <param name="logger"></param>
        public StoreController(IFilmlistRepository filmlistRepository, ILogger<StoreController> logger)
        {
            // Initialize the Filmlist Repository and Logger
            _filmlistRepository = filmlistRepository;
            _logger = logger;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Index Action Method for the Store Controller
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <param name="SearchQuery"></param>
        /// <param name="Category"></param>
        /// <param name="Rating"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] int pageNo = 1, [FromQuery] int pageSize = 10, [FromQuery] string? SearchQuery = null, [FromQuery] string? Category = null, [FromQuery] string? Rating = null)
        {
            try
            {
                _logger.LogInformation("Store Index Page Entered");
                _logger.LogDebug("Page Number: {PageNumber}, Page Size: {PageSize}, Search Query: {SearchQuery}", pageNo, pageSize, SearchQuery);

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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load the store index page");
                // Optionally provide an error model to a custom error view
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        #endregion Public Methods
    }
}