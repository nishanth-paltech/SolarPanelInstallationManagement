using Microsoft.EntityFrameworkCore;
using SolarPanelInstallationManagement.Data;
using SolarPanelInstallationManagement.Repositories.Contracts;
using SolarPanelInstallationManagement.Repositories.Implementations;
using SolarPanelInstallationManagement.Services.Contracts;
using SolarPanelInstallationManagement.Services.Implementations;

namespace SolarPanelInstallationManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("SolarPanelInstallationDbConn"),
                    x => x.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
                )
            );

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IConsumerSurveyRepository, ConsumerSurveyRepository>();
            builder.Services.AddScoped<IConsumerSurveyService, ConsumerSurveyService>();


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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // //Seed data insertions from excel
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

            app.Run();
        }
    }
}
