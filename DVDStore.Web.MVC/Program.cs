namespace DVDStore.Web.MVC;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();
        //=====================================================================================
        // Call MapControllers to map attribute routed controllers.
        // Ref: https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/routing?view=aspnetcore-6.0
        // Using this with Attribute Routing gives us better explicit control with the routing in our application.
        // Call MapControllers to map attribute routed controllers.
        //=====================================================================================
        app.MapControllers();
        //=====================================================================================
        // The URL path / uses the route template default Home controllers and Index action. The
        // URL path /Home uses the route template default Index action.
        //
        // Here we use the convenience method MapDefaultControllerRoute:
        app.MapDefaultControllerRoute();
        //=====================================================================================

        //=====================================================================================
        // Non Attribute Routing would be done as follows with EACH AREA having and individual
        // call to the MapControllerRoute.  Along with the creation of the "default" route.
        //=====================================================================================
        // AREA Routing See: https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/areas?view=aspnetcore-6.0
        //app.MapControllerRoute(
        //      name: "Residents",
        //      pattern: "{area:exists}/{controller=Residents}/{action=Index}/{id?}");
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
