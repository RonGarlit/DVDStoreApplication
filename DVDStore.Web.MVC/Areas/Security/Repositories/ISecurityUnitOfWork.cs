namespace DVDStore.Web.MVC.Areas.Security.Repositories
{
    public interface ISecurityUnitOfWork
    {
        IRoleRepository Role { get; }
        IUserRepository User { get; }
    }
}

