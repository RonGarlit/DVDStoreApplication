
// ReSharper disable All

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DVDStore.DAL
{
    // ****************************************************************************************************
    // DVDStore DAL Code
    // ****************************************************************************************************

    public partial class DVDStoreDbContext : DbContext, IDVDStoreDbContext
    {
        public DVDStoreDbContext()
        {
            InitializePartial();
        }

        public DVDStoreDbContext(DbContextOptions<DVDStoreDbContext> options)
            : base(options)
        {
            InitializePartial();
        }

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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=(local);Initial Catalog=DVDStore;Integrated Security=True;MultipleActiveResultSets=True;Encrypt=false;TrustServerCertificate=true");
            }
        }

        public bool IsSqlParameterNull(SqlParameter param)
        {
            var sqlValue = param.SqlValue;
            var nullableValue = sqlValue as INullable;
            if (nullableValue != null)
                return nullableValue.IsNull;
            return (sqlValue == null || sqlValue == DBNull.Value);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ActorMap());
            modelBuilder.ApplyConfiguration(new AddressMap());
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new CityMap());
            modelBuilder.ApplyConfiguration(new CountryMap());
            modelBuilder.ApplyConfiguration(new CustomerMap());
            modelBuilder.ApplyConfiguration(new CustomerlistMap());
            modelBuilder.ApplyConfiguration(new FilmMap());
            modelBuilder.ApplyConfiguration(new FilmactorMap());
            modelBuilder.ApplyConfiguration(new FilmcategoryMap());
            modelBuilder.ApplyConfiguration(new FilmlistMap());
            modelBuilder.ApplyConfiguration(new FilmRevMap());
            modelBuilder.ApplyConfiguration(new FilmtextMap());
            modelBuilder.ApplyConfiguration(new InventoryMap());
            modelBuilder.ApplyConfiguration(new LanguageMap());
            modelBuilder.ApplyConfiguration(new LogMap());
            modelBuilder.ApplyConfiguration(new PaymentMap());
            modelBuilder.ApplyConfiguration(new RentalMap());
            modelBuilder.ApplyConfiguration(new SalesbyfilmcategoryMap());
            modelBuilder.ApplyConfiguration(new SalesbystoreMap());
            modelBuilder.ApplyConfiguration(new StaffMap());
            modelBuilder.ApplyConfiguration(new StafflistMap());
            modelBuilder.ApplyConfiguration(new StoreMap());
            modelBuilder.ApplyConfiguration(new TempActorFilmListingMap());

            modelBuilder.Entity<UspGetDatabaseStatisticsReturnModel>().HasNoKey();
            modelBuilder.Entity<UspGetListOfTitlesByCategoryReturnModel>().HasNoKey();

            OnModelCreatingPartial(modelBuilder);
        }


        partial void InitializePartial();
        partial void DisposePartial(bool disposing);
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        static partial void OnCreateModelPartial(ModelBuilder modelBuilder, string schema);

        // Stored Procedures
        public int Insertlog(string level, string callSite, string type, string message, string stackTrace, string innerException, string additionalInfo)
        {
            var levelParam = new SqlParameter { ParameterName = "@level", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = level, Size = -1 };
            if (levelParam.Value == null)
                levelParam.Value = DBNull.Value;

            var callSiteParam = new SqlParameter { ParameterName = "@callSite", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = callSite, Size = -1 };
            if (callSiteParam.Value == null)
                callSiteParam.Value = DBNull.Value;

            var typeParam = new SqlParameter { ParameterName = "@type", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = type, Size = -1 };
            if (typeParam.Value == null)
                typeParam.Value = DBNull.Value;

            var messageParam = new SqlParameter { ParameterName = "@message", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = message, Size = -1 };
            if (messageParam.Value == null)
                messageParam.Value = DBNull.Value;

            var stackTraceParam = new SqlParameter { ParameterName = "@stackTrace", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = stackTrace, Size = -1 };
            if (stackTraceParam.Value == null)
                stackTraceParam.Value = DBNull.Value;

            var innerExceptionParam = new SqlParameter { ParameterName = "@innerException", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = innerException, Size = -1 };
            if (innerExceptionParam.Value == null)
                innerExceptionParam.Value = DBNull.Value;

            var additionalInfoParam = new SqlParameter { ParameterName = "@additionalInfo", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = additionalInfo, Size = -1 };
            if (additionalInfoParam.Value == null)
                additionalInfoParam.Value = DBNull.Value;

            var procResultParam = new SqlParameter { ParameterName = "@procResult", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };

            Database.ExecuteSqlRaw("EXEC @procResult = [dbo].[Insertlog] @level, @callSite, @type, @message, @stackTrace, @innerException, @additionalInfo", levelParam, callSiteParam, typeParam, messageParam, stackTraceParam, innerExceptionParam, additionalInfoParam, procResultParam);

            return (int)procResultParam.Value;
        }

        // InsertlogAsync() cannot be created due to having out parameters, or is relying on the procedure result (int)

        public List<UspGetDatabaseStatisticsReturnModel> UspGetDatabaseStatistics()
        {
            int procResult;
            return UspGetDatabaseStatistics(out procResult);
        }

        public List<UspGetDatabaseStatisticsReturnModel> UspGetDatabaseStatistics(out int procResult)
        {
            var procResultParam = new SqlParameter { ParameterName = "@procResult", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
            const string sqlCommand = "EXEC @procResult = [dbo].[usp_GetDatabaseStatistics]";
            var procResultData = Set<UspGetDatabaseStatisticsReturnModel>()
                .FromSqlRaw(sqlCommand, procResultParam)
                .ToList();

            procResult = (int) procResultParam.Value;
            return procResultData;
        }

        public async Task<List<UspGetDatabaseStatisticsReturnModel>> UspGetDatabaseStatisticsAsync()
        {
            const string sqlCommand = "EXEC [dbo].[usp_GetDatabaseStatistics]";
            var procResultData = await Set<UspGetDatabaseStatisticsReturnModel>()
                .FromSqlRaw(sqlCommand)
                .ToListAsync();

            return procResultData;
        }

        public List<UspGetListOfTitlesByCategoryReturnModel> UspGetListOfTitlesByCategory(string categoryName)
        {
            int procResult;
            return UspGetListOfTitlesByCategory(categoryName, out procResult);
        }

        public List<UspGetListOfTitlesByCategoryReturnModel> UspGetListOfTitlesByCategory(string categoryName, out int procResult)
        {
            var categoryNameParam = new SqlParameter { ParameterName = "@categoryName", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = categoryName, Size = 25 };
            if (categoryNameParam.Value == null)
                categoryNameParam.Value = DBNull.Value;

            var procResultParam = new SqlParameter { ParameterName = "@procResult", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
            const string sqlCommand = "EXEC @procResult = [dbo].[usp_GetListOfTitlesByCategory] @categoryName";
            var procResultData = Set<UspGetListOfTitlesByCategoryReturnModel>()
                .FromSqlRaw(sqlCommand, categoryNameParam, procResultParam)
                .ToList();

            procResult = (int) procResultParam.Value;
            return procResultData;
        }

        public async Task<List<UspGetListOfTitlesByCategoryReturnModel>> UspGetListOfTitlesByCategoryAsync(string categoryName)
        {
            var categoryNameParam = new SqlParameter { ParameterName = "@categoryName", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = categoryName, Size = 25 };
            if (categoryNameParam.Value == null)
                categoryNameParam.Value = DBNull.Value;

            const string sqlCommand = "EXEC [dbo].[usp_GetListOfTitlesByCategory] @categoryName";
            var procResultData = await Set<UspGetListOfTitlesByCategoryReturnModel>()
                .FromSqlRaw(sqlCommand, categoryNameParam)
                .ToListAsync();

            return procResultData;
        }

    }
}
// </auto-generated>
