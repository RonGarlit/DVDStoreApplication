using DVDStore.Web.MVC.Areas.Store.Common;
using DVDStore.Web.MVC.Areas.Store.Models;

namespace DVDStore.Web.MVC.Areas.Store.Repositories
{
    public interface IFilmlistRepository
    {
        Task<StorePagedModel<FilmlistViewModel>> GetPagedFilmlists(StoreResourceParameters resourceParameters);
    }
}