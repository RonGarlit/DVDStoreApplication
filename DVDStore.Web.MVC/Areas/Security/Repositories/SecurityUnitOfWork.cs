namespace DVDStore.Web.MVC.Areas.Security.Repositories
{
    public class SecurityUnitOfWork : ISecurityUnitOfWork
    {
        public IUserRepository User { get; }
        public IRoleRepository Role { get; }

        public SecurityUnitOfWork(IUserRepository user, IRoleRepository role)
        {
            User = user;
            Role = role;
        }
    }
}
