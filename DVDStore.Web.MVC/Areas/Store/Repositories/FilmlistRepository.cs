using DVDStore.DAL;
using DVDStore.Web.MVC.Areas.Store.Common;
using DVDStore.Web.MVC.Areas.Store.Models;
using DVDStore.Web.MVC.Common.Extensions;
using DVDStore.Web.MVC.Common.PropertyMapping;
using DVDStore.Web.MVC.Common.PropertyMapping.BaseMappingCode;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVDStore.Web.MVC.Areas.Store.Repositories
{
    public class FilmlistRepository : IFilmlistRepository
    {
        private readonly DVDStoreDbContext _context;
        private readonly FilmlistPropertyMapper _propertyMapper;

        public FilmlistRepository(DVDStoreDbContext context, FilmlistPropertyMapper propertyMapper)
        {
            _context = context;
            _propertyMapper = propertyMapper;
        }

        public async Task<StorePagedModel<FilmlistViewModel>> GetPagedFilmlists(StoreResourceParameters resourceParameters)
        {
            var collectionBeforePaging = _context.Filmlists.AsQueryable();

            // Apply filtering
            if (!string.IsNullOrEmpty(resourceParameters.SearchQuery))
            {
                var searchQuery = resourceParameters.SearchQuery.Trim().ToLowerInvariant();
                collectionBeforePaging = collectionBeforePaging
                    .Where(f => f.Title.ToLower().Contains(searchQuery) ||
                                f.Description.ToLower().Contains(searchQuery));
            }

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

            return new StorePagedModel<FilmlistViewModel>(filmlistViewModels, filmlistPagedModel.TotalCount, filmlistPagedModel.CurrentPage, filmlistPagedModel.PageSize);
        }

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
    }
}
