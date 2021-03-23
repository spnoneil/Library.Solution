using Library.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
namespace Library
{
  public class Startup
  {
    public Startup(IWebHostEnvironment env)
    {
      var builder = new ConfigurationBuilder()
          .SetBasePath(env.ContentRootPath)
          .AddJsonFile("appsettings.json");
      Configuration = builder.Build();
    }

    public IConfigurationRoot Configuration { get; set; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc();

      services.AddEntityFrameworkMySql()
        .AddDbContext<LibraryContext>(options => options
        .UseMySql(Configuration["ConnectionStrings:DefaultConnection"], ServerVersion.AutoDetect(Configuration["ConnectionStrings:DefaultConnection"])));

      //new code
    services.AddIdentity<ApplicationUser, IdentityRole>()
        //add roles
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<LibraryContext>()
        .AddDefaultTokenProviders();

      services.Configure<IdentityOptions>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 0;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredUniqueChars = 0;
    });
    }

    private async Task CreateRoles(IServiceProvider serviceProvider)
    {
      //initializing custom roles 
      var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
      var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
      string[] roleNames = { "Admin", "Manager", "Member" };
      IdentityResult roleResult;

      foreach (var roleName in roleNames)
      {
      var roleExist = await RoleManager.RoleExistsAsync(roleName);
      if (!roleExist)
      {
          //create the roles and seed them to the database: Question 1
          roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
      }
      }

      //Here you could create a super user who will maintain the web app
      var poweruser = new ApplicationUser
      {
        UserName = "Librarian",
        Email = "Librarian",
        };
        //Ensure you have these values in your appsettings.json file
        string userPWD = "LibraryPassword";
        var _user = await UserManager.FindByEmailAsync("Librarian");

        if(_user == null)
        {
          var createPowerUser = await UserManager.CreateAsync(poweruser, userPWD);
          if (createPowerUser.Succeeded)
          {
            await UserManager.AddToRoleAsync(poweruser, "Admin");
          }
        }
    }

    public void Configure(IApplicationBuilder app)
    {
      app.UseDeveloperExceptionPage();

      app.UseAuthentication(); 

      app.UseRouting();
      app.UseAuthorization();

      app.UseEndpoints(routes =>
      {
        routes.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
      });

      app.UseStaticFiles();
      
      app.Run(async (context) =>
      {
        await context.Response.WriteAsync("Hello World!");
      });
    }
  }
}