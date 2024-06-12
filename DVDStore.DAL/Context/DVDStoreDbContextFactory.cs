
// ReSharper disable All

using Microsoft.EntityFrameworkCore.Design;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVDStore.DAL
{
    public partial class DVDStoreDbContextFactory : IDesignTimeDbContextFactory<DVDStoreDbContext>
    {
        public DVDStoreDbContext CreateDbContext(string[] args)
        {
            return new DVDStoreDbContext();
        }
    }
}
// </auto-generated>
