using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SolarPanelInstallationManagement.Data;
using SolarPanelInstallationManagement.Models.DTOs.Account;
using SolarPanelInstallationManagement.Models.Entities;
using SolarPanelInstallationManagement.Repositories.Contracts;
using SolarPanelInstallationManagement.Repositories.Implementations;
using SolarPanelInstallationManagement.Services.Contracts;
using SolarPanelInstallationManagement.Services.Implementations;

namespace SolarPanelInstallationManagement
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                options.Filters.Add(new AuthorizeFilter(policy));
            });

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("SolarPanelInstallationDbConn"),
                    x => x.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
                )
            );

            builder.Services
            .AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // Password settings (simple but secure)
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = true;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = false;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";

                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.SlidingExpiration = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IConsumerSurveyRepository, ConsumerSurveyRepository>();
            builder.Services.AddScoped<IConsumerSurveyService, ConsumerSurveyService>();
            builder.Services.AddScoped<IConsumerSurveyAttachmentService,
                           ConsumerSurveyAttachmentService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();   // MUST be before UseAuthorization
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=ConsumerSurvey}/{action=Index}/{id?}");

            //Seed data insertions from excel
            //using (var scope = app.Services.CreateScope())
            //{
            //    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            //    if (!db.ConsumerSurveys.Any())
            //    {
            //        db.ConsumerSurveys.AddRange(
            //            ConsumerSurveySeedData.Get()
            //        );
            //        db.SaveChanges();
            //    }
            //}
            await IdentitySeeder.SeedAdminAsync(app.Services);
            app.Run();
        }
    }
}
