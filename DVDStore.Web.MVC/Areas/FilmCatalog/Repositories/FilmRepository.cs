using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DVDStore.Web.MVC.Areas.FilmCatalog.Models;
using DVDStore.Web.MVC.Areas.FilmCatalog.Common;
using DVDStore.Web.MVC.Common;
using DVDStore.DAL;

namespace DVDStore.Web.MVC.Areas.FilmCatalog.Repositories
{
    public class FilmRepository : IFilmRepository
    {
        private readonly IDVDStoreDbContext _context;

        public FilmRepository(IDVDStoreDbContext context)
        {
            _context = context;
        }

        public async Task<PagedList<DetailsFilmModel>> GetAllFilmsAsync(FilmCatalogResourceParameters parameters)
        {
            var collection = _context.Films
                .ApplySort(parameters.SortOrder)
                .Select(f => new DetailsFilmModel
                {
                    FilmId = f.Filmid,
                    Title = f.Title,
                    Description = f.Description,
                    Genre = f.Filmcategories.FirstOrDefault().Category.Name, // Assuming Genre is derived from the Category relationship
                    RentalRate = f.Rentalrate,
                    Length = (f.Length ?? 0),
                    Rating = f.Rating,
                    LastUpdate = f.Lastupdate
                });

            if (!string.IsNullOrEmpty(parameters.SearchQuery))
            {
                collection = collection.Where(f => f.Title.Contains(parameters.SearchQuery)
                                                   || f.Description.Contains(parameters.SearchQuery));
            }

            if (!string.IsNullOrEmpty(parameters.Genre))
            {
                collection = collection.Where(f => f.Genre == parameters.Genre);
            }

            if (!string.IsNullOrEmpty(parameters.Rating))
            {
                collection = collection.Where(f => f.Rating == parameters.Rating);
            }

            return await Task.FromResult(PagedList<DetailsFilmModel>.Create(collection, parameters.PageNumber, parameters.PageSize));
        }

        public async Task<DetailsFilmModel> GetFilmByIdAsync(int filmId)
        {
            var film = await _context.Films.FindAsync(filmId);

            if (film == null) return null;

            return new DetailsFilmModel
            {
                FilmId = film.Filmid,
                Title = film.Title,
                Description = film.Description,
                Genre = film.Filmcategories.FirstOrDefault()?.Category.Name, // Assuming Genre is derived from the Category relationship
                RentalRate = film.Rentalrate,
                Length = (film.Length ?? 0),
                Rating = film.Rating,
                LastUpdate = film.Lastupdate
            };
        }

        public async Task AddFilmAsync(Film film)
        {
            await _context.Films.AddAsync(film);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFilmAsync(Film film)
        {
            _context.Films.Update(film);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFilmAsync(int filmId)
        {
            var film = await _context.Films.FindAsync(filmId);
            if (film != null)
            {
                _context.Films.Remove(film);
                await _context.SaveChangesAsync();
            }
        }
    }
}
