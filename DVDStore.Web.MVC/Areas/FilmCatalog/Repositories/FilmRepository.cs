using Microsoft.EntityFrameworkCore;
using DVDStore.Web.MVC.Areas.FilmCatalog.Common;
using DVDStore.Web.MVC.Areas.FilmCatalog.Models;
using DVDStore.DAL;
using DVDStore.Web.MVC.Common.Extensions;

namespace DVDStore.Web.MVC.Areas.FilmCatalog.Repositories
{
    public class FilmRepository : IFilmRepository
    {
        private readonly DVDStoreDbContext _context;
        private readonly FilmsPropertyMapper _propertyMapper;

        public FilmRepository(DVDStoreDbContext context, FilmsPropertyMapper propertyMapper)
        {
            _context = context;
            _propertyMapper = propertyMapper;
        }

        public Task<FilmsPagedModel<FilmViewModel>> GetPagedFilms(FilmCatalogResourceParameters resourceParameters)
        {
            var collectionBeforePaging = _context.Films.AsQueryable();

            // Apply filtering
            if (!string.IsNullOrEmpty(resourceParameters.SearchQuery))
            {
                var searchQuery = resourceParameters.SearchQuery.Trim().ToLowerInvariant();
                collectionBeforePaging = collectionBeforePaging
                    .Where(f => f.Title.ToLowerInvariant().Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                                f.Description.ToLowerInvariant().Contains(searchQuery, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(resourceParameters.Rating))
            {
                var ratingForWhereClause = resourceParameters.Rating.Trim();
                collectionBeforePaging = collectionBeforePaging
                    .Where(f => string.Equals(f.Rating, ratingForWhereClause, StringComparison.OrdinalIgnoreCase));
            }

            // Apply sorting
            collectionBeforePaging = collectionBeforePaging.ApplySort(resourceParameters.SortOrder, _propertyMapper.GetPropertyMapping<Film, Film>());

            var filmsPagedModel = FilmsPagedModel<Film>.Create(collectionBeforePaging, resourceParameters.PageNumber, resourceParameters.PageSize);

            var filmViewModels = filmsPagedModel.Select(f => MapToViewModel(f)).ToList();

            return Task.FromResult(new FilmsPagedModel<FilmViewModel>(filmViewModels, filmsPagedModel.TotalCount, filmsPagedModel.CurrentPage, filmsPagedModel.PageSize));
        }

        public async Task<FilmViewModel> GetFilm(int id)
        {
            var film = await _context.Films.FindAsync(id);
            if (film == null)
            {
                return null!;
            }
            return MapToViewModel(film);
        }


        public async Task<FilmViewModel> AddFilm(FilmViewModel filmViewModel)
        {
            var film = MapToDomainModel(filmViewModel);
            _context.Films.Add(film);
            await _context.SaveChangesAsync();
            return MapToViewModel(film);
        }

        public async Task<FilmViewModel> UpdateFilm(FilmViewModel filmViewModel)
        {
            var film = await _context.Films.FindAsync(filmViewModel.Filmid);
            if (film == null)
            {
                return null!;
            }

            _context.Entry(film).CurrentValues.SetValues(filmViewModel);
            await _context.SaveChangesAsync();
            return MapToViewModel(film);
        }


        public async Task<bool> DeleteFilm(int id)
        {
            var film = await _context.Films.Include(f => f.Filmcategories).FirstOrDefaultAsync(f => f.Filmid == id);
            if (film == null) return false;

            // Delete related Filmcategory entities
            _context.Filmcategories.RemoveRange(film.Filmcategories);

            _context.Films.Remove(film);
            await _context.SaveChangesAsync();
            return true;
        }


        private static FilmViewModel MapToViewModel(Film film)
        {
            return new FilmViewModel
            {
                Filmid = film.Filmid,
                Title = film.Title,
                Description = film.Description,
                Releaseyear = film.Releaseyear,
                Languageid = film.Languageid,
                Originallanguageid = film.Originallanguageid,
                Rentalduration = film.Rentalduration,
                Rentalrate = film.Rentalrate,
                Length = film.Length,
                Replacementcost = film.Replacementcost,
                Rating = film.Rating,
                Specialfeatures = film.Specialfeatures,
                Lastupdate = film.Lastupdate
            };
        }

        private static Film MapToDomainModel(FilmViewModel viewModel)
        {
            return new Film
            {
                Filmid = viewModel.Filmid,
                Title = viewModel.Title,
                Description = viewModel.Description,
                Releaseyear = viewModel.Releaseyear,
                Languageid = viewModel.Languageid,
                Originallanguageid = viewModel.Originallanguageid,
                Rentalduration = viewModel.Rentalduration,
                Rentalrate = viewModel.Rentalrate,
                Length = viewModel.Length,
                Replacementcost = viewModel.Replacementcost,
                Rating = viewModel.Rating,
                Specialfeatures = viewModel.Specialfeatures,
                Lastupdate = viewModel.Lastupdate
            };
        }
    }
}
