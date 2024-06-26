
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

    public partial class FakeDVDStoreDbContext : IDVDStoreDbContext
    {
        public DbSet<Actor> Actors { get; set; } // actor
        public DbSet<Address> Addresses { get; set; } // address
        public DbSet<Category> Categories { get; set; } // category
        public DbSet<City> Cities { get; set; } // city
        public DbSet<Country> Countries { get; set; } // country
        public DbSet<Customer> Customers { get; set; } // customer
        public DbSet<Customerlist> Customerlists { get; set; } // customerlist
        public DbSet<Film> Films { get; set; } // film
        public DbSet<Filmactor> Filmactors { get; set; } // filmactor
        public DbSet<Filmcategory> Filmcategories { get; set; } // filmcategory
        public DbSet<Filmlist> Filmlists { get; set; } // filmlist
        public DbSet<FilmRev> FilmRevs { get; set; } // filmRev
        public DbSet<Filmtext> Filmtexts { get; set; } // filmtext
        public DbSet<Inventory> Inventories { get; set; } // inventory
        public DbSet<Language> Languages { get; set; } // language
        public DbSet<Log> Logs { get; set; } // logs
        public DbSet<Payment> Payments { get; set; } // payment
        public DbSet<Rental> Rentals { get; set; } // rental
        public DbSet<Salesbyfilmcategory> Salesbyfilmcategories { get; set; } // salesbyfilmcategory
        public DbSet<Salesbystore> Salesbystores { get; set; } // salesbystore
        public DbSet<Staff> Staffs { get; set; } // staff
        public DbSet<Stafflist> Stafflists { get; set; } // stafflist
        public DbSet<Store> Stores { get; set; } // store
        public DbSet<TempActorFilmListing> TempActorFilmListings { get; set; } // tempActorFilmListing

        public FakeDVDStoreDbContext()
        {
            _database = new FakeDatabaseFacade(new DVDStoreDbContext());

            Actors = new FakeDbSet<Actor>("Actorid");
            Addresses = new FakeDbSet<Address>("Addressid");
            Categories = new FakeDbSet<Category>("Categoryid");
            Cities = new FakeDbSet<City>("Cityid");
            Countries = new FakeDbSet<Country>("Countryid");
            Customers = new FakeDbSet<Customer>("Customerid");
            Customerlists = new FakeDbSet<Customerlist>();
            Films = new FakeDbSet<Film>("Filmid");
            Filmactors = new FakeDbSet<Filmactor>("Actorid", "Filmid");
            Filmcategories = new FakeDbSet<Filmcategory>("Filmid", "Categoryid");
            Filmlists = new FakeDbSet<Filmlist>();
            FilmRevs = new FakeDbSet<FilmRev>("Filmid", "Title", "Rentalduration", "Rentalrate", "Replacementcost", "Categoryid", "Name");
            Filmtexts = new FakeDbSet<Filmtext>("Filmid");
            Inventories = new FakeDbSet<Inventory>("Inventoryid");
            Languages = new FakeDbSet<Language>("Languageid");
            Logs = new FakeDbSet<Log>("Logid");
            Payments = new FakeDbSet<Payment>("Paymentid");
            Rentals = new FakeDbSet<Rental>("Rentalid");
            Salesbyfilmcategories = new FakeDbSet<Salesbyfilmcategory>();
            Salesbystores = new FakeDbSet<Salesbystore>();
            Staffs = new FakeDbSet<Staff>("Staffid");
            Stafflists = new FakeDbSet<Stafflist>();
            Stores = new FakeDbSet<Store>("Storeid");
            TempActorFilmListings = new FakeDbSet<TempActorFilmListing>("ActorId", "FirstName", "LastName", "FilmId", "FilmTitle");

            InitializePartial();
        }

        public int SaveChangesCount { get; private set; }
        public virtual int SaveChanges()
        {
            ++SaveChangesCount;
            return 1;
        }

        public virtual int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return SaveChanges();
        }

        public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            ++SaveChangesCount;
            return Task<int>.Factory.StartNew(() => 1, cancellationToken);
        }
        public virtual Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken)
        {
            ++SaveChangesCount;
            return Task<int>.Factory.StartNew(x => 1, acceptAllChangesOnSuccess, cancellationToken);
        }

        partial void InitializePartial();

        protected virtual void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private DatabaseFacade _database;
        public DatabaseFacade Database { get { return _database; } }

        public DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public virtual EntityEntry Add(object entity)
        {
            throw new NotImplementedException();
        }

        public virtual EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public virtual Task AddRangeAsync(params object[] entities)
        {
            throw new NotImplementedException();
        }

        public virtual async Task AddRangeAsync(IEnumerable<object> entities, CancellationToken cancellationToken = default)
        {
            await Task.CompletedTask;
            throw new NotImplementedException();
        }

        public virtual async ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class
        {
            await Task.CompletedTask;
            throw new NotImplementedException();
        }

        public virtual async ValueTask<EntityEntry> AddAsync(object entity, CancellationToken cancellationToken = default)
        {
            await Task.CompletedTask;
            throw new NotImplementedException();
        }

        public virtual void AddRange(IEnumerable<object> entities)
        {
            throw new NotImplementedException();
        }

        public virtual void AddRange(params object[] entities)
        {
            throw new NotImplementedException();
        }

        public virtual EntityEntry Attach(object entity)
        {
            throw new NotImplementedException();
        }

        public virtual EntityEntry<TEntity> Attach<TEntity>(TEntity entity) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public virtual void AttachRange(IEnumerable<object> entities)
        {
            throw new NotImplementedException();
        }

        public virtual void AttachRange(params object[] entities)
        {
            throw new NotImplementedException();
        }

        public virtual EntityEntry Entry(object entity)
        {
            throw new NotImplementedException();
        }

        public virtual EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public virtual TEntity Find<TEntity>(params object[] keyValues) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public virtual ValueTask<TEntity> FindAsync<TEntity>(object[] keyValues, CancellationToken cancellationToken) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public virtual ValueTask<TEntity> FindAsync<TEntity>(params object[] keyValues) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public virtual ValueTask<object> FindAsync(Type entityType, object[] keyValues, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public virtual ValueTask<object> FindAsync(Type entityType, params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public virtual object Find(Type entityType, params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public virtual EntityEntry Remove(object entity)
        {
            throw new NotImplementedException();
        }

        public virtual EntityEntry<TEntity> Remove<TEntity>(TEntity entity) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public virtual void RemoveRange(IEnumerable<object> entities)
        {
            throw new NotImplementedException();
        }

        public virtual void RemoveRange(params object[] entities)
        {
            throw new NotImplementedException();
        }

        public virtual EntityEntry Update(object entity)
        {
            throw new NotImplementedException();
        }

        public virtual EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public virtual void UpdateRange(IEnumerable<object> entities)
        {
            throw new NotImplementedException();
        }

        public virtual void UpdateRange(params object[] entities)
        {
            throw new NotImplementedException();
        }

        public virtual IQueryable<TResult> FromExpression<TResult> (Expression<Func<IQueryable<TResult>>> expression)
        {
            throw new NotImplementedException();
        }


        // Stored Procedures

        public int Insertlog(string level, string callSite, string type, string message, string stackTrace, string innerException, string additionalInfo)
        {
            return 0;
        }

        // InsertlogAsync() cannot be created due to having out parameters, or is relying on the procedure result (int)

        public DbSet<UspGetDatabaseStatisticsReturnModel> UspGetDatabaseStatisticsReturnModel { get; set; }
        public List<UspGetDatabaseStatisticsReturnModel> UspGetDatabaseStatistics()
        {
            int procResult;
            return UspGetDatabaseStatistics(out procResult);
        }

        public List<UspGetDatabaseStatisticsReturnModel> UspGetDatabaseStatistics(out int procResult)
        {
            procResult = 0;
            return new List<UspGetDatabaseStatisticsReturnModel>();
        }

        public Task<List<UspGetDatabaseStatisticsReturnModel>> UspGetDatabaseStatisticsAsync()
        {
            int procResult;
            return Task.FromResult(UspGetDatabaseStatistics(out procResult));
        }

        public DbSet<UspGetListOfTitlesByCategoryReturnModel> UspGetListOfTitlesByCategoryReturnModel { get; set; }
        public List<UspGetListOfTitlesByCategoryReturnModel> UspGetListOfTitlesByCategory(string categoryName)
        {
            int procResult;
            return UspGetListOfTitlesByCategory(categoryName, out procResult);
        }

        public List<UspGetListOfTitlesByCategoryReturnModel> UspGetListOfTitlesByCategory(string categoryName, out int procResult)
        {
            procResult = 0;
            return new List<UspGetListOfTitlesByCategoryReturnModel>();
        }

        public Task<List<UspGetListOfTitlesByCategoryReturnModel>> UspGetListOfTitlesByCategoryAsync(string categoryName)
        {
            int procResult;
            return Task.FromResult(UspGetListOfTitlesByCategory(categoryName, out procResult));
        }
    }
}
// </auto-generated>
