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
**  FileName: FilmRepository.cs (DVDStore Application)
**  Version: 0.1
**  Author: Ronald Garlit
**
**  Description: Film Repository for the FilmCatalog Area of the DVDStore Application.
**
**  Change History
**
**  WHEN			WHO        WHAT
**---------------------------------------------------------------------------------
**  2024-05-31		RGARLIT     STARTED DEVELOPMENT
***********************************************************************************/

using DVDStore.DAL;
using DVDStore.Web.MVC.Areas.FilmCatalog.Common;
using DVDStore.Web.MVC.Areas.FilmCatalog.Models;
using DVDStore.Web.MVC.Common.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DVDStore.Web.MVC.Areas.FilmCatalog.Repositories
{
    public class FilmRepository : IFilmRepository
    {
        #region Private Fields

        private readonly DVDStoreDbContext _context;
        private readonly FilmsPropertyMapper _propertyMapper;

        #endregion Private Fields

        #region Public Constructors

        public FilmRepository(DVDStoreDbContext context, FilmsPropertyMapper propertyMapper)
        {
            _context = context;
            _propertyMapper = propertyMapper;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<FilmViewModel> AddFilm(FilmViewModel filmViewModel)
        {
            var film = MapToDomainModel(filmViewModel);
            await _context.Films.AddAsync(film);
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

        public async Task<FilmViewModel> GetFilm(int id)
        {
            var film = await _context.Films.FindAsync(id);
            if (film == null)
            {
                return null!;
            }
            return MapToViewModel(film);
        }

        public async Task<FilmsPagedModel<FilmViewModel>> GetPagedFilms(FilmCatalogResourceParameters resourceParameters)
        {
            // Fetch all films
            var collectionBeforePaging = _context.Films.AsQueryable();

            // Apply filtering
            if (!string.IsNullOrEmpty(resourceParameters.SearchQuery))
            {
                var searchQuery = resourceParameters.SearchQuery.Trim();
                collectionBeforePaging = collectionBeforePaging
                    .Where(f => f.Title.Contains(searchQuery)
                    || f.Description.Contains(searchQuery)
                    || f.Rating.Contains(searchQuery));
            }

            // Apply sorting
            collectionBeforePaging = collectionBeforePaging.ApplySort(resourceParameters.SortOrder, _propertyMapper.GetPropertyMapping<Film, Film>());

            // Create paged model
            var filmsPagedModel = await FilmsPagedModel<Film>.CreateAsync(collectionBeforePaging, resourceParameters.PageNumber, resourceParameters.PageSize);

            // Map to view model
            var filmViewModels = filmsPagedModel.Select(f => MapToViewModel(f)).ToList();

            return new FilmsPagedModel<FilmViewModel>(filmViewModels, filmsPagedModel.TotalCount, filmsPagedModel.CurrentPage, filmsPagedModel.PageSize);
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

        #endregion Public Methods

        #region Private Methods

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

        #endregion Private Methods
    }
}