using System.Threading.Tasks;
using DVDStore.Web.MVC.Areas.FilmCatalog.Models;
using DVDStore.Web.MVC.Areas.FilmCatalog.Common;
using DVDStore.DAL;
using DVDStore.Web.MVC.Common;

namespace DVDStore.Web.MVC.Areas.FilmCatalog.Repositories
{
    public interface IFilmRepository
    {
        Task<PagedList<DetailsFilmModel>> GetAllFilmsAsync(FilmCatalogResourceParameters parameters);

        Task<DetailsFilmModel> GetFilmByIdAsync(int filmId);

        Task AddFilmAsync(Film film);

        Task UpdateFilmAsync(Film film);

        Task DeleteFilmAsync(int filmId);
    }
}