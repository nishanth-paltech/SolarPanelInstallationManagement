using Microsoft.EntityFrameworkCore;
using SolarPanelInstallationManagement.Models.Entities;
using SolarPanelInstallationManagement.Repositories.Contracts;
using System.Reflection.Emit;

namespace SolarPanelInstallationManagement.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<ConsumerSurvey> ConsumerSurveys => Set<ConsumerSurvey>();
        public DbSet<ConsumerSurveyAttachment> ConsumerSurveyAttachments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
                {
                    var method = typeof(AppDbContext)
                        .GetMethod(nameof(SetSoftDeleteFilter), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)!
                        .MakeGenericMethod(entityType.ClrType);

                    method.Invoke(null, new object[] { modelBuilder });
                }
            }

            modelBuilder.Entity<ConsumerSurveyAttachment>()
                .HasOne(a => a.ConsumerSurvey)
                .WithMany(s => s.Attachments)
                .HasForeignKey(a => a.ConsumerSurveySno)
                .HasPrincipalKey(s => s.Sno);
        }

        private static void SetSoftDeleteFilter<TEntity>(ModelBuilder builder) where TEntity : class, ISoftDelete
        {
            builder.Entity<TEntity>().HasQueryFilter(e => !e.IsDeleted);
        }
    }
}
