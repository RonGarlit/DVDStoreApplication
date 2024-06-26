
// ReSharper disable All

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DVDStore.DAL
{
    // ****************************************************************************************************
    // DVDStore DAL Code
    // ****************************************************************************************************

    public partial interface IDVDStoreDbContext : IDisposable
    {
        DbSet<Actor> Actors { get; set; } // actor
        DbSet<Address> Addresses { get; set; } // address
        DbSet<Category> Categories { get; set; } // category
        DbSet<City> Cities { get; set; } // city
        DbSet<Country> Countries { get; set; } // country
        DbSet<Customer> Customers { get; set; } // customer
        DbSet<Customerlist> Customerlists { get; set; } // customerlist
        DbSet<Film> Films { get; set; } // film
        DbSet<Filmactor> Filmactors { get; set; } // filmactor
        DbSet<Filmcategory> Filmcategories { get; set; } // filmcategory
        DbSet<Filmlist> Filmlists { get; set; } // filmlist
        DbSet<FilmRev> FilmRevs { get; set; } // filmRev
        DbSet<Filmtext> Filmtexts { get; set; } // filmtext
        DbSet<Inventory> Inventories { get; set; } // inventory
        DbSet<Language> Languages { get; set; } // language
        DbSet<Log> Logs { get; set; } // logs
        DbSet<Payment> Payments { get; set; } // payment
        DbSet<Rental> Rentals { get; set; } // rental
        DbSet<Salesbyfilmcategory> Salesbyfilmcategories { get; set; } // salesbyfilmcategory
        DbSet<Salesbystore> Salesbystores { get; set; } // salesbystore
        DbSet<Staff> Staffs { get; set; } // staff
        DbSet<Stafflist> Stafflists { get; set; } // stafflist
        DbSet<Store> Stores { get; set; } // store
        DbSet<TempActorFilmListing> TempActorFilmListings { get; set; } // tempActorFilmListing

        int SaveChanges();
        int SaveChanges(bool acceptAllChangesOnSuccess);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken));
        DatabaseFacade Database { get; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        string ToString();

        EntityEntry Add(object entity);
        EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class;
        Task AddRangeAsync(params object[] entities);
        Task AddRangeAsync(IEnumerable<object> entities, CancellationToken cancellationToken = default);
        ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class;
        ValueTask<EntityEntry> AddAsync(object entity, CancellationToken cancellationToken = default);
        void AddRange(IEnumerable<object> entities);
        void AddRange(params object[] entities);

        EntityEntry Attach(object entity);
        EntityEntry<TEntity> Attach<TEntity>(TEntity entity) where TEntity : class;
        void AttachRange(IEnumerable<object> entities);
        void AttachRange(params object[] entities);

        EntityEntry Entry(object entity);
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        TEntity Find<TEntity>(params object[] keyValues) where TEntity : class;
        ValueTask<TEntity> FindAsync<TEntity>(object[] keyValues, CancellationToken cancellationToken) where TEntity : class;
        ValueTask<TEntity> FindAsync<TEntity>(params object[] keyValues) where TEntity : class;
        ValueTask<object> FindAsync(Type entityType, object[] keyValues, CancellationToken cancellationToken);
        ValueTask<object> FindAsync(Type entityType, params object[] keyValues);
        object Find(Type entityType, params object[] keyValues);

        EntityEntry Remove(object entity);
        EntityEntry<TEntity> Remove<TEntity>(TEntity entity) where TEntity : class;
        void RemoveRange(IEnumerable<object> entities);
        void RemoveRange(params object[] entities);

        EntityEntry Update(object entity);
        EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class;
        void UpdateRange(IEnumerable<object> entities);
        void UpdateRange(params object[] entities);

        IQueryable<TResult> FromExpression<TResult> (Expression<Func<IQueryable<TResult>>> expression);

        // Stored Procedures
        int Insertlog(string level, string callSite, string type, string message, string stackTrace, string innerException, string additionalInfo);
        // InsertlogAsync() cannot be created due to having out parameters, or is relying on the procedure result (int)

        List<UspGetDatabaseStatisticsReturnModel> UspGetDatabaseStatistics();
        List<UspGetDatabaseStatisticsReturnModel> UspGetDatabaseStatistics(out int procResult);
        Task<List<UspGetDatabaseStatisticsReturnModel>> UspGetDatabaseStatisticsAsync();

        List<UspGetListOfTitlesByCategoryReturnModel> UspGetListOfTitlesByCategory(string categoryName);
        List<UspGetListOfTitlesByCategoryReturnModel> UspGetListOfTitlesByCategory(string categoryName, out int procResult);
        Task<List<UspGetListOfTitlesByCategoryReturnModel>> UspGetListOfTitlesByCategoryAsync(string categoryName);

    }
}
// </auto-generated>
