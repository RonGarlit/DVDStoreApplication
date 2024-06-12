using DVDStore.Web.MVC.Areas.Identity.Data;
using DVDStore.Web.MVC.Areas.Security.Models;
using DVDStore.Web.MVC.Common;

namespace DVDStore.Web.MVC.Areas.Security.Repositories
{
    public interface IUserRepository
    {
        ApplicationUser DeleteUser(ApplicationUser user);

        ApplicationUser GetUser(string id);

        UsersPagedModel<ApplicationUser> GetUsersPagedList(UsersResourceParameters UsersResourceParameters);

        ApplicationUser UpdateUser(ApplicationUser user);
    }
}