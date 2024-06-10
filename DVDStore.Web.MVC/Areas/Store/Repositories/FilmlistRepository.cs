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
**  FileName: FilmlistRepository.cs (DVDStore Application)
**  Version: 1.0
**  Author: Ronald Garlit
**
**  Description:
**  This file contains the FilmlistRepository class which implements the
**  IFilmlistRepository interface. This repository handles CRUD operations
**  and pagination for the Filmlist entities. It includes methods for filtering,
**  sorting, and mapping between Filmlist entities and FilmlistViewModel.
**
**  Change History
**
**  WHEN            WHO          WHAT
**---------------------------------------------------------------------------------
**  2024-05-31      RGARLIT      STARTED DEVELOPMENT
***********************************************************************************/

using DVDStore.DAL;
using DVDStore.Web.MVC.Areas.Store.Common;
using DVDStore.Web.MVC.Areas.Store.Models;
using DVDStore.Web.MVC.Common.Extensions;

namespace DVDStore.Web.MVC.Areas.Store.Repositories
{
    /// <summary>
    /// Filmlist repository class that implements the IFilmlistRepository interface.
    /// </summary>
    public class FilmlistRepository : IFilmlistRepository
    {
        #region Private Fields

        // DVDStoreDbContext instance
        private readonly DVDStoreDbContext _context;

        // FilmlistPropertyMapper instance
        private readonly FilmlistPropertyMapper _propertyMapper;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// FilmlistRepository constructor that initializes the DVDStoreDbContext and FilmlistPropertyMapper instances.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="propertyMapper"></param>
        public FilmlistRepository(DVDStoreDbContext context, FilmlistPropertyMapper propertyMapper)
        {
            // Initialize the DVDStoreDbContext and FilmlistPropertyMapper instances.
            _context = context;
            _propertyMapper = propertyMapper;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// GetPagedFilmlists method that returns a paged model of FilmlistViewModels.
        /// </summary>
        /// <param name="resourceParameters"></param>
        /// <returns></returns>
        public async Task<StorePagedModel<FilmlistViewModel>> GetPagedFilmlists(StoreResourceParameters resourceParameters)
        {
            var collectionBeforePaging = _context.Filmlists.AsQueryable();

            // Apply filtering
            if (!string.IsNullOrEmpty(resourceParameters.SearchQuery))
            {
                var searchQuery = resourceParameters.SearchQuery.Trim().ToLowerInvariant();
                collectionBeforePaging = collectionBeforePaging
                    .Where(f => f.Title.ToLower().Contains(searchQuery)
                    || f.Description.ToLower().Contains(searchQuery)
                    || f.Actors.ToLower().Contains(searchQuery));
            }
            // Apply category filtering
            if (!string.IsNullOrEmpty(resourceParameters.Category))
            {
                var categoryForWhereClause = resourceParameters.Category.Trim().ToLowerInvariant();
                collectionBeforePaging = collectionBeforePaging
                    .Where(f => f.Category.ToLower() == categoryForWhereClause);
            }

            // Apply sorting
            collectionBeforePaging = collectionBeforePaging.ApplySort(resourceParameters.SortOrder, _propertyMapper.GetPropertyMapping<Filmlist, Filmlist>());

            // Create paged model
            var filmlistPagedModel = await StorePagedModel<Filmlist>.CreateAsync(collectionBeforePaging, resourceParameters.PageNumber, resourceParameters.PageSize);

            // Map to view model
            var filmlistViewModels = filmlistPagedModel.Select(f => MapToViewModel(f)).ToList();
            // Return paged model of FilmlistViewModels
            return new StorePagedModel<FilmlistViewModel>(filmlistViewModels, filmlistPagedModel.TotalCount, filmlistPagedModel.CurrentPage, filmlistPagedModel.PageSize);
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// MapToEntity method that maps a FilmlistViewModel to a Filmlist entity.
        /// </summary>
        /// <param name="filmlistViewModel"></param>
        /// <returns></returns>
        private Filmlist MapToEntity(FilmlistViewModel filmlistViewModel)
        {
            return new Filmlist
            {
                Fid = filmlistViewModel.Fid,
                Title = filmlistViewModel.Title,
                Description = filmlistViewModel.Description,
                Category = filmlistViewModel.Category,
                Price = filmlistViewModel.Price,
                Length = filmlistViewModel.Length,
                Rating = filmlistViewModel.Rating,
                Actors = filmlistViewModel.Actors
            };
        }

        /// <summary>
        /// MapToViewModel method that maps a Filmlist entity to a FilmlistViewModel.
        /// </summary>
        /// <param name="filmlist"></param>
        /// <returns></returns>
        private FilmlistViewModel MapToViewModel(Filmlist filmlist)
        {
            return new FilmlistViewModel
            {
                Fid = filmlist.Fid,
                Title = filmlist.Title,
                Description = filmlist.Description,
                Category = filmlist.Category,
                Price = filmlist.Price,
                Length = filmlist.Length,
                Rating = filmlist.Rating,
                Actors = filmlist.Actors
            };
        }

        #endregion Private Methods
    }
}

/*
                      (   (            )  )   _____ ____
THE VEX FILES  #3      \_               _/   ///=- \-=- \  .------------.
         ___           ( "--         --" )  ///|==\//=\\ \ | It must be |
        /---\\--.       ( (      )    ) )   ||//==\//=\\\\\| demonic    |
       // /==\/-\\      \_     (       _/   \\\/     \\\|||| posession. |
      ///=/     \ \     ( "--    )  --" )    \/ _  __ \///|`-. .--------'
     |_/=/ ._  _.\=      ( (   )     ) )      \"\  /"-.\|//  |/
     / /=   ." . /=\     \_   (  (    _/      /`/ /`   ||/   '
    / / /     \  \\ \    ( "   (  )  " )     / /       .)
   | /|(.     "   || |    )(  ))) )) )(      |(__.)\  /|
   \  \\|    /-\  |/ /  __\ (()(((()( /__  _ |  ___,   |  _
    \\_\\\_  `-' /_//  |.-'   """"""  `-.| \"-\  - ____/-"/
     "   /\`\___/|/\   ||   -._ .-.     ||__\|/`---' /\| /____
   _..--'\ \  .  / /--.||   -._| | |    ||   \\ \ _ / ///     "\
  /       \_\---/   *. ||   -._|"|"|    || .*  \_/ \_///__      |
 |        \  \ /      .||   -._|.-.|    ||.      \_/ /   /      |
 |   |     \  /    *.  ||_______________||  .*   / \|   /   Y   |
            \    *.  . /.-.-.-.-.-.-.-.-_\ .     | ||  /    |   |
   DrS.             . /.-.-.-.-.-.-.-.-._\\ .*        /         |
                  *. /.-.-.-.-.-.-.-.-.-.-.\ .*
             ( (    /_____/___________\___o_\    ) )
                    \_______________________/

               Scurvy suspects a more mundane reason
               for the self-destruction of her laptop.

*/