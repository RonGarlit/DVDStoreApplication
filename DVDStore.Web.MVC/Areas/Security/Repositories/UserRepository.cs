using DVDStore.Web.MVC.Areas.Identity.Data;
using DVDStore.Web.MVC.Areas.Security.Models;
using DVDStore.Web.MVC.Common.Extensions;
using DVDStore.Web.MVC.Common;

namespace DVDStore.Web.MVC.Areas.Security.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SecurityDbContext _context;
        private readonly UsersPropertyMapper _UsersPropertyMapper;

        public UserRepository(SecurityDbContext context)
        {
            _context = context;
            _UsersPropertyMapper = new UsersPropertyMapper();
        }

        public ICollection<ApplicationUser> GetUsers()
        {
            return _context.Users.ToList();
        }

        public ApplicationUser GetUser(string id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id)!;
        }

        public UsersPagedModel<ApplicationUser> GetUsersPagedList(UsersResourceParameters UsersResourceParameters)
        {
            //=================================================================
            // Check parameters
            //=================================================================

            // Check for null using ArgumentNullException throw helper
            ArgumentNullException.ThrowIfNull(UsersResourceParameters);

            // Setup IQueryable for table object we are going to get data for.
            // We load the collection variable accordingly as we process the resource
            // parameter object passed into the repository
            var collection = _context.Users as IQueryable<ApplicationUser>;

            // Comment on filtering logic:
            // Filtering might be applied based on specific attributes such as first name, last name, or school/college.
            // This would involve checking if certain filter parameters are not null or whitespace,
            // and then trimming these parameters to use in a conditional query that narrows down the collection.

            // Check and run for the search parameter and get the collection for
            // columns we have chosen to allow search-able
            if (!string.IsNullOrWhiteSpace(UsersResourceParameters.SearchQuery))
            {
                // Get and clean up the Search Query
                var searchQuery = UsersResourceParameters.SearchQuery.Trim();
                // Build out the IQueryable collection in EF LINQ of the columns
                // we want to search here.
                collection = collection.Where(a => a.FirstName!.Contains(searchQuery)
                || a.LastName!.Contains(searchQuery)
                || a.Email!.Contains(searchQuery));
            }

            // Next check the orderby parameter and then apply the sort
            if (!string.IsNullOrWhiteSpace(UsersResourceParameters.OrderBy))
            {
                // get property mapping dictionary
                // Not using DTO right now but my mapper is set for other stuff involved in search.
                var UsersPropertyMappingDictionary = _UsersPropertyMapper.GetPropertyMapping<ApplicationUser, ApplicationUser>();

                collection = collection.ApplySort(UsersResourceParameters.OrderBy, UsersPropertyMappingDictionary);
            }

            // FINALLY run the collection through the Paging process
            return UsersPagedModel<ApplicationUser>.Create(collection, UsersResourceParameters.PageNumber, UsersResourceParameters.PageSize);
        }


        public ApplicationUser UpdateUser(ApplicationUser user)
        {
            _context.Update(user);
            _context.SaveChanges();

            return user;
        }
        public ApplicationUser DeleteUser(ApplicationUser user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();

            return user;
        }
    }
}
