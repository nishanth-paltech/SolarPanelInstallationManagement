using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SolarPanelInstallationManagement.Models.Entities;

namespace SolarPanelInstallationManagement.Data.Configurations
{
    public class ConsumerSurveyConfiguration : IEntityTypeConfiguration<ConsumerSurvey>
    {
        public void Configure(EntityTypeBuilder<ConsumerSurvey> builder)
        {
            builder.ToTable("consumer_survey");

            builder.HasKey(x => x.Sno);

            builder.Property(x => x.Sno)
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.District)
                   .HasMaxLength(100);

            builder.Property(x => x.Constituency)
                   .HasMaxLength(100);

            builder.Property(x => x.Mandal)
                   .HasMaxLength(100);

            builder.Property(x => x.Village)
                   .HasMaxLength(100);

            builder.Property(x => x.ConsumerNameWithSurname)
                   .HasMaxLength(200);

            builder.Property(x => x.USCNO)
                   .HasMaxLength(50);

            builder.Property(x => x.ExistingContractedLoadKW)
                   .HasColumnType("numeric(10,2)");

            builder.Property(x => x.ConsumerMobileNo)
                   .HasMaxLength(15);

            builder.Property(x => x.AadharNo)
                   .HasMaxLength(20);

            builder.Property(x => x.LatitudeLongitude)
                   .HasMaxLength(50);

            builder.Property(x => x.TypeOfRoof)
                   .HasMaxLength(50);

            builder.Property(x => x.ShadeFreeRoofAreaSqFt)
                   .HasColumnType("numeric(10,2)");

            builder.Property(x => x.Remarks)
                   .HasMaxLength(500);

            builder.Property(x => x.SSName)
                   .HasMaxLength(150);

            builder.Property(x => x.FeederName)
                   .HasMaxLength(150);

            builder.Property(x => x.ServiceNumber)
                   .HasMaxLength(50);

            builder.Property(x => x.Category)
                   .HasMaxLength(50);

            builder.Property(x => x.PoleNumber)
                   .HasMaxLength(50);

            builder.Property(x => x.DtrStructureCode)
                   .HasMaxLength(100);

            builder.Property(x => x.ExistingDtrCapacityKva)
                   .HasColumnType("numeric(10,2)");

            builder.Property(x => x.CreatedOn)
                   .HasDefaultValueSql("NOW()");

            builder.Property(x => x.UpdatedOn);

            builder.Property(x => x.IsDeleted)
                .HasDefaultValue(false);
        }
    }
}
