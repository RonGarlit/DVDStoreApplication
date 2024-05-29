using DVDStore.Web.MVC.Areas.FilmCatalog.Common;
using DVDStore.Web.MVC.Areas.FilmCatalog.Models;

namespace DVDStore.Web.MVC.Areas.FilmCatalog.Repositories
{
    public interface IFilmRepository
    {
        Task<FilmViewModel> AddFilm(FilmViewModel filmViewModel);
        Task<bool> DeleteFilm(int id);
        Task<FilmViewModel> GetFilm(int id);
        Task<FilmsPagedModel<FilmViewModel>> GetPagedFilms(FilmCatalogResourceParameters resourceParameters);
        Task<FilmViewModel> UpdateFilm(FilmViewModel filmViewModel);
    }
}