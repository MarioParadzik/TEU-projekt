using Microsoft.EntityFrameworkCore;
using CyberBlog.DbContexts;
using Auth0.AspNetCore.Authentication;
using CyberBlog.Helpers;

namespace CyberBlog;
public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<CyberDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("CyberDB")));
        services.ConfigureSameSiteNoneCookies();
        services.AddAuth0WebAppAuthentication(options =>
        {
            options.Domain = Configuration["Auth0:Domain"];
            options.ClientId = Configuration["Auth0:ClientId"];
            options.Scope = "openid profile email";
        });
        services.AddControllersWithViews();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (!env.IsDevelopment())
        {
            app.UseExceptionHandler("/Blog/Error");
        }

        app.UseStaticFiles();
        app.UseCookiePolicy();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Blog}/{action=Index}/{id?}");
        });
    }

}