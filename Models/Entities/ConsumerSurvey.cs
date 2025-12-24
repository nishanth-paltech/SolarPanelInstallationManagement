using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SolarPanelInstallationManagement.Repositories.Contracts;

namespace SolarPanelInstallationManagement.Models.Entities
{
    [Table("consumer_survey")]
    public class ConsumerSurvey : ISoftDelete
    {
        [Key]
        public int Sno { get; set; }

        [MaxLength(100)]
        public string? District { get; set; }

        [MaxLength(100)]
        public string? Constituency { get; set; }

        [MaxLength(100)]
        public string? Mandal { get; set; }

        [MaxLength(100)]
        public string? Village { get; set; }

        [MaxLength(200)]
        public string? ConsumerNameWithSurname { get; set; }

        [MaxLength(50)]
        public string? USCNO { get; set; }

        [Column(TypeName = "numeric(10,2)")]
        public decimal? ExistingContractedLoadKW { get; set; }

        public bool? HasOtherDomesticServices { get; set; }

        [MaxLength(15)]
        public string? ConsumerMobileNo { get; set; }

        [MaxLength(20)]
        public string? AadharNo { get; set; }

        public bool? IsBeneficiaryUnderGJScheme { get; set; }

        [MaxLength(50)]
        public string? LatitudeLongitude { get; set; }

        [MaxLength(50)]
        public string? TypeOfRoof { get; set; }

        [Column(TypeName = "numeric(10,2)")]
        public decimal? ShadeFreeRoofAreaSqFt { get; set; }

        [MaxLength(500)]
        public string? Remarks { get; set; }

        public bool? IsNameChangeRequired { get; set; }

        [MaxLength(150)]
        public string? SSName { get; set; }

        [MaxLength(150)]
        public string? FeederName { get; set; }

        [MaxLength(50)]
        public string? ServiceNumber { get; set; }

        [MaxLength(50)]
        public string? Category { get; set; }

        [MaxLength(50)]
        public string? PoleNumber { get; set; }

        [MaxLength(100)]
        public string? DtrStructureCode { get; set; }

        [Column(TypeName = "numeric(10,2)")]
        public decimal? ExistingDtrCapacityKva { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedOn { get; set; }
        public bool IsDeleted { get; set; } = false;
        public ICollection<ConsumerSurveyAttachment> Attachments { get; set; }
    = new List<ConsumerSurveyAttachment>();
    }
}
