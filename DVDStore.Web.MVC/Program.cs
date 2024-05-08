using DVDStore.DAL;
using DVDStore.Web.MVC.Common.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using System;

namespace DVDStore.Web.MVC;

public static class Program
{
    public static void Main(string[] args)
    {
        //=====================================================================================
        // NLog: setup the logger first to catch all errors
        //=====================================================================================
        // Using nlog.config for configuration, so no need to load from appsettings.json explicitly.
        var logger = NLog.LogManager.Setup().LoadConfigurationFromFile("nlog.config", optional: false).GetCurrentClassLogger();

        logger.Debug("Init main method in the program.cs");
        try
        {
            // Log the start of the application
            logger.Debug("init main");
            // Create the host builder
            var builder = WebApplication.CreateBuilder(args);
            // This line already exists in your code, ensuring configuration is available
            var config = builder.Configuration;

            //=====================================================================================
            // Add services to the container.
            //=====================================================================================
            // Dependency Injection Area
            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-6.0
            //=====================================================================================
            // Register the DbContext class
            // Here we are registering DbContext class
            builder.Services.AddScoped<IDVDStoreDbContext, DVDStoreDbContext>();



            // Initialize NLog for ASP.NET Core and add it to the builder
            builder.Logging.ClearProviders(); // Remove other loggers from the builder
            builder.Host.UseNLog();  // This ensures NLog is used throughout the application

            // Add services to the container and configure the application
            builder.Services.AddControllersWithViews();  // Add MVC services to the container

            // Build the application and create the host instance
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                // Use the exception handler middleware to log exceptions
                app.UseExceptionHandler("/Home/Error"); // Use the home controller to handle errors
                // Enable HSTS to ensure the application is only accessible over HTTPS
                app.UseHsts();
            }
            // Redirect HTTP requests to HTTPS
            app.UseHttpsRedirection();
            // Serve static files
            app.UseStaticFiles();
            // Enable routing
            app.UseRouting();

            app.UseAuthorization();

            // Add endpoints to the request pipeline to handle requests to controllers and Razor pages
            app.MapControllers(); // Add controllers to the request pipeline
            app.MapDefaultControllerRoute(); // Add a default controller route

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            // Log the start of the application
            logger.Debug("start main");
            // Read the value from the configuration
            bool triggerIntentionalException = config.GetValue<bool>("TriggerIntentionalException");

            // Check if the TriggerIntentionalException is set to true in the appsettings.ENVIRONMENT.json file.
            if (!triggerIntentionalException)
            {
                // Run the application
                app.Run();
            }
            else
            {
                // Trigger the intentional exception for testing
                throw new IntentionalException("Intentional Exception for testing of Global Exception Handler");
            }
        }
        catch (Exception ex)
        {
            // Use the already declared logger to log the exception and throw it
            logger.Error(ex, "Stopped program because of exception");
            // Perform any necessary cleanup here before exiting
            // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
            NLog.LogManager.Shutdown();
        }
        finally
        {
            logger.Debug("exit main");
            logger.Debug("Shutting down NLog");
            // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
            NLog.LogManager.Shutdown();
        }
    }
}
