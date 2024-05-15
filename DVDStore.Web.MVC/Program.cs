using DVDStore.DAL;
using DVDStore.Web.MVC.Areas.Identity.Data;
using DVDStore.Web.MVC.Areas.Identity.Services;
using DVDStore.Web.MVC.Areas.Security.Repositories;
using DVDStore.Web.MVC.Common.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
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
            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-8.0
            //=====================================================================================
            // Register the DbContext class
            // Here we are registering DbContext class
            builder.Services.AddScoped<IDVDStoreDbContext, DVDStoreDbContext>();

            // Register Repository Classes
            builder.Services.AddScoped<ISecurityUnitOfWork, SecurityUnitOfWork>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();

            //=====================================================================================
            // Add Security/Identity DbContext Area
            //=====================================================================================
            // Retrieves the value with the specified key from the ConnectionStrings section of the
            // configuration source. Calling this method is shorthand for GetSection("ConnectionStrings")[name].
            var connectionString = builder.Configuration.GetConnectionString("SecurityDbContextConnection") ?? throw new InvalidOperationException("Connection string 'SecurityDbContextConnection' not found.");

            // Registers the given context as a service in the IServiceCollection.
            builder.Services.AddDbContext<SecurityDbContext>(options =>
                    options.UseSqlServer(connectionString));

            // Adds a set of common identity services to the application, including a default UI,
            // token providers, and configures authentication to use identity cookies.
            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                //***Note: The order is critical. AddRoles must come before AddEntityFrameworkStores***
                // Adds Role related services for TRole, including IRoleStore, IRoleValidator, and RoleManager.
                .AddRoles<IdentityRole>()
                //Adds an Entity Framework implementation of identity information stores.
                .AddEntityFrameworkStores<SecurityDbContext>()
                // Adds the default token providers used to generate tokens for reset passwords,
                // change email and change telephone number operations, and for two factor
                // authentication token generation.
                .AddDefaultTokenProviders();

            // Registers services required by authentication services.
            builder.Services.AddAuthentication();

            //=====================================================================================
            // Add MAIN DbContext Area
            //=====================================================================================
            // Get our DVDStoreDb connection string from the appsettings.json file
            var DVDStoreDbConnectionString = builder.Configuration.GetConnectionString("DVDStoreDb") ?? throw new InvalidOperationException("Connection string 'DVDStoreDb' not found.");
            // Register our DVDStoreDBContext from the DAL project
            builder.Services.AddDbContext<DVDStoreDbContext>(options =>
                options.UseSqlServer(DVDStoreDbConnectionString));

            //=====================================================================================
            // Developer Exception Page Middle-ware - This middle-ware displays a detailed HTML
            // error page when an unhanded exception occurs in the web app. The error page includes
            // detailed exception information and stack traces. The error page is made available only
            //  to local requests. You can use the DeveloperExceptionPageOptions to control the
            //  behavior of the error page.
            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            }

            //=====================================================================================
            // Add services to the container.
            //=====================================================================================
            // We are specifically using AddControllersWithViews method.
            // It does not support Page (RazorPages) which we are avoiding intentionally here. These
            // FOUR methods "AddController vs AddMvc vs AddControllersWithViews vs AddRazorPages"
            // support different things. See this article for details.
            // Link: https://dotnettutorials.net/lesson/difference-between-addmvc-and-addmvccore-method/
            //=====================================================================================
            // set cache to Cache-Control: max-age=0, must-revalidate
            // https://stackoverflow.com/questions/42097802/apply-a-browser-caching-policy-to-all-asp-net-core-mvc-pages
            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new ResponseCacheAttribute() { NoStore = true, Location = ResponseCacheLocation.None });
            });

            // NLog: setup the logger first to catch all errors
            logger.Info("Configuring Entity Framework Core logging");
            // The following line uses NLog.Web to automatically set the configuration of NLog
            builder.Logging.Services.AddSingleton<ILoggerProvider>(new NLogLoggerProvider(new NLogProviderOptions
            {
                CaptureMessageTemplates = true,
                CaptureMessageProperties = true
            }));

            //=====================================================================================
            // NLog: Setup NLog for Dependency injection
            //=====================================================================================
            builder.Logging.ClearProviders();
            // NLog: setup the logger first to catch all errors
            builder.Host.UseNLog();

            // Add services to the container and configure the application
            builder.Services.AddControllersWithViews();  // Add MVC services to the container

            //=====================================================================================
            // Set the APP variable with the newly created WebApplication Builder class instance
            // https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.webapplicationbuilder?view=aspnetcore-6.0
            //=====================================================================================
            logger.Info("Application Configuration Code Completed - Running WebApplication Build Method ");
            // Build the WebApplication and return it
            var app = builder.Build();
            //=====================================================================================
            // Configure the HTTP request pipeline.
            //=====================================================================================
            // For production environments newly implementing HTTPS, it is advisable to start with a 
            // conservative initial HSTS max-age value. This could be set to as little as a few hours or 
            // a single day to allow for easy reversion to HTTP if necessary. Once the HTTPS setup is 
            // verified as stable, the HSTS max-age can be increased to a standard duration, such as one 
            // year, to enhance security.
            //
            // Implementing the HSTS header involves setting the Strict-Transport-Security response header
            // with desired directives like preload, includeSubDomains, and an appropriate max-age. 
            // It is also possible to specify domains that should be excluded from the policy.
            //
            // Detailed guidance on configuring HSTS in ASP.NET Core can be found in the official 
            // Microsoft documentation available online.
            //
            // Reference Link:
            // - [Enabling HSTS in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/security/enforcing-ssl?view=aspnetcore-8.0&tabs=visual-studio%2Clinux-ubuntu#hsts)
            //=====================================================================================


            //=====================================================================================
            // Configure the HTTP request pipeline. ASP.NET Core implements HSTS with the UseHsts
            // extension method. The following code calls UseHsts when the app isn't in development mode.
            //=====================================================================================

            // Setup Middle-ware and configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                logger.Info("Application Environment Code - Running in Development Environment ");
                app.UseMigrationsEndPoint();  // This is the default migration page for development
                app.UseDeveloperExceptionPage(); // This is the default exception page for development
            }
            else
            {
                logger.Info($"Application Environment Code - Running in {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")} Environment ");
                // Exception Handling Middle-ware - This middle-ware catches exceptions that occur in
                // the request pipeline and generates an error page response. The error page is made
                // available only to local requests. You can use the ExceptionHandlerOptions to control
                // the behavior of the error page.
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production
                // scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Adds middle-ware for redirecting HTTP Requests to HTTPS.
            app.UseHttpsRedirection();
            // Enables static file serving for the current request path
            app.UseStaticFiles();
            //  Defines the point in the middle-ware pipeline where routing decisions are made, and
            //  an Endpoint is associated with the HttpContext.
            //  Adds middle-ware for routing requests to controller actions.
            app.UseRouting();
            //  Adds the AuthenticationMiddleware to the specified IApplicationBuilder, which
            //  enables authentication capabilities.
            app.UseAuthentication();
            //  Adds the AuthorizationMiddleware to the specified IApplicationBuilder, which
            //  enables authorization capabilities.
            app.UseAuthorization();

            //=====================================================================================
            // Call MapControllers to map attribute routed controllers.
            // Ref: https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/routing?view=aspnetcore-8.0
            // Using this with Attribute Routing gives us better explicit control with the routing in our application.
            // Call MapControllers to map attribute routed controllers.
            //=====================================================================================
            app.MapControllers();
            //=====================================================================================
            // Here we use the convenience method MapDefaultControllerRoute to map conventional
            // routed controllers. This method is equivalent to calling MapControllerRoute with the
            // name "default" and the template "{controller=Home}/{action=Index}/{id?}".
            // https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.controllerendpointroutebuilderextensions.mapdefaultcontrollerroute?view=aspnetcore-8.0
            //=====================================================================================
            app.MapDefaultControllerRoute();
            //=====================================================================================
            // Adds endpoints for Razor Pages
            //=====================================================================================
            // MapRazorPages is also used by the Identity.UI stuff.
            app.MapRazorPages();
            //=====================================================================================
            // End Middle-ware configuration
            //=====================================================================================

            // Insert the new seeding logic here for users and roles
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    IdentityDataInitializer.SeedData(userManager, roleManager).Wait();
                    logger.Info("Identity data seeded successfully.");
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "An error occurred while seeding the identity data.");
                }
            }


            logger.Info("Application Middle-ware Setup Completed - Starting the WebApplication via Run Method");
            // Run the application
            logger.Info("Application starting up");
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
        catch (Exception exception)
        {
            logger.Error("Global Error Handler Triggered - Saving Information to the Logs");
            logger.Fatal(exception, "Stopped program because of exception");
        }
        finally
        {
            logger.Warn("Application shutting down");
            // Ensure to flush and stop internal timers/threads before
            // application-exit (Avoid segmentation fault on Linux)
            NLog.LogManager.Shutdown();  // Flushes any pending log messages before shutting down.
        }
    }
}

/*
 
  
    	Millennium Falcon
  
                              ____          ____
                             / --.|        |.-- \
                            /,---||        ||---.\
                           //    ||        ||    \\
                          //     ||        ||     \\
                         // __   ||        ||   __ \\
                        //,'  `. ||        || ,'||`.\\
                       //( \`--|)||        ||( \'|  )\\
                      //  `.-_,' ||________|| `.-_,'  \\
                     //        | ||   _|   ||          \\
                    //         | || | .. | ||           \\
                   //          | || | :: | ||            \\
                  /    __     888|| |  _ | ||        __    \
                 /   ,'|_'. _ 888|| |  | | ||  _   ,'-.`.   \
                /   ( ,-._ | |888|| | -| | || |_| (   |==)   \
               /     `O__,'|_|8@ || | [| | |@ |_|  `._|,'     \
              /            __..|--| | )| | |--|_|__            \           ,==.
             /      __,--''      |  |  | |  |      ``--.__      \         //[]\\
            /    ,::::'          |-=|  | |  |::::..       `-.    \       //||||\\
           /  ,-':::::       ..::|  | /| |  |::::::::        `-.  \     ||,'  `.||
          / ,'    ''       ::::::|  | \| |  |  ''::::     oo    `. \    |--------|
         /,'           ,'\ :::'''|] |  | |  |   ....    o8888o    `.\   |:::_[[[[]
        /'           ,'   )      |> |  | | [|  ::::::     88888  ..  \  |    |   ]
       /           ,'     |      |] |=_| | [|  '::::'oo     8   ::::  \-'    |---|
      .'     o   ,'    \_/       | :| \| |.[|      o8888o        ::;-'      ,'   |
     ,'     o88,'   `._/:\|      | :|  | |: |     88888888      ,-'  \  _,-'     |
     |     8888 ``--'|::::|      |_:|  | |:_|       8888     ,-'   \ ,-'   .::_,-'
    ,'    8888      ''--""      || || [| || ||            ,-'    _,-'   \ _::'
    |                           || |_,--._| ||         ,-'  \ ,-'     _,-'
    |                           |__(______)__|      ,-'   _,-' .::_,-'   |
   |'                  88o     =====,,--..=====  .-'   ,-' \  _::'       `|
   |                  888888o    ,-'\    /`-.     \ \-' . _,-'            |
   `---._____        888888888 ,' / [HHHH[=====-   \ |_::'       _____.---'
     __|_____`=======._____   /   \ [HHHH[=====-    `'__.======='_____|__
    {I |  _______________ || /    /\/____\/\:.  \ || _______________  | I}
    [I |  :___]__[_______ ||(    /   _.._   \:.  )|| ______]___[___:  | I]
    {I_|____________o8o___|| \  /:  /\  /\   \: / ||__________________|_I}
       |_____,------'         \/:'  \ \/ /    \/:..   88`-------._____|
   .---'   ::::::              `.   [||||]   ,'  '::..                `---.
   |          '::..::        ,'  `-.      ,-'  `.  ':'                    |
   |.        .:::''        ,' __    ``--''    __ `.     .::.             ,|
    |        '''         ,' ,'||`.    __    ,'||`. `.    '::::.          |
    |                  ,'  (||||||) ,'||`. (||||||) 88.    '::::.        |
    `.               ,'     `.||,' (||||||) `.||,' 88888.    ':'        ,'
     |             ,'      __       `.||,'       __  88  `.             |
     `.          ,'      ,'||`.       __       ,'||`.      `.          ,'
      `.       ,'       (||||||)    ,'||`.    (||||||)       `.       ,'
        \    ,'          `.||,'    (||||||)    `.||,'          `.    /
         `.  |                      `.||,'                      |  ,'
           `-|                                                  |-'
             `.   ,'.                                    .`.   ,'
               `-:,'o88o ,/                        \.     `.;-'
                  `-888 //      /     ||ooo  \ oo88 \\ __,-'
                       `--..__ /'     ||888  `\ oo8;--'
                              ```-----''-----''''
 
 */