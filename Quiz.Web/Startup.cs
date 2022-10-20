using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Portland.Data.Repository;
using QA_Synoptic.Maps.Properties;
using Quiz.Data.QA;
using Quiz.Data.Repository;
using Quiz.Data.Repository.IRepository;
using System.Configuration;
using System.Security.Claims;

namespace Quiz.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            var builder = WebApplication.CreateBuilder();

            // Add services to the container.
            services.AddControllersWithViews();
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Maps());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);
            builder.Services.AddAuthorization();

            builder.Services.AddAuthentication("EditCookie").AddCookie("EditCookie", options =>
            {
                options.Cookie.Name = "EditCookie";
                options.ExpireTimeSpan = TimeSpan.FromDays(365);
                options.Cookie.MaxAge = TimeSpan.FromDays(365);
            });
            builder.Services.AddAuthentication("ViewCookie").AddCookie("ViewCookie", options =>
            {
                options.Cookie.Name = "ViewCookie";
                options.ExpireTimeSpan = TimeSpan.FromDays(365);
                options.Cookie.MaxAge = TimeSpan.FromDays(365);
            });
            builder.Services.AddAuthentication("RestrictedCookie").AddCookie("RestrictedCookie", options =>
            {
                options.Cookie.Name = "RestrictedCookie";
                options.ExpireTimeSpan = TimeSpan.FromDays(365);
                options.Cookie.MaxAge = TimeSpan.FromDays(365);
            });

            builder.Services.AddMvc();
            builder.Services.AddScoped<IQAUnitOfWork, QAUnitOfWork>();
            builder.Services.AddDbContext<QuizContext>(options =>
                       options.UseNpgsql(
                           Configuration.GetConnectionString("QAQuizDb")));
            
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
            app.UseAuthorization();

            //app.MapGet("/", () => "Hello World! identity");
            //app.MapGet("/login", (HttpContext ctx) =>
            //{
            //    ctx.SignInAsync(new ClaimsPrincipal(new[]{new ClaimsIdentity(new List<Claim>()
            //    {
            //        new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
            //    },
            //        CookieAuthenticationDefaults.AuthenticationScheme)
            //    }));
            //});

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            //app.MapControllerRoute(
            //    name: "Other",
            //    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });
            app.MapControllerRoute(
                name: "MyArea",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


            app.Run();
        }
    }

}

