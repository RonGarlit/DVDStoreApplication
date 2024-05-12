using DVDStore.Web.MVC.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace DVDStore.Web.MVC.Areas.Security.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly SecurityDbContext _context;

        public RoleRepository(SecurityDbContext context)
        {
            _context = context;
        }

        public ICollection<IdentityRole> GetRoles()
        {
            return _context.Roles.ToList();
        }
    }
}
