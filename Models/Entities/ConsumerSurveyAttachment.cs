using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SolarPanelInstallationManagement.Repositories.Contracts;

namespace SolarPanelInstallationManagement.Models.Entities
{
    [Table("consumer_survey_attachment")]
    public class ConsumerSurveyAttachment: ISoftDelete
    {
        [Key]
        public int Id { get; set; }

        // FK
        [Required]
        public int ConsumerSurveySno { get; set; }

        [ForeignKey(nameof(ConsumerSurveySno))]
        public ConsumerSurvey ConsumerSurvey { get; set; }

        // File metadata
        [Required, StringLength(255)]
        public string OriginalFileName { get; set; }

        [Required, StringLength(255)]
        public string StoredFileName { get; set; }

        [Required, StringLength(500)]
        public string FilePath { get; set; }

        [Required, StringLength(100)]
        public string ContentType { get; set; }

        public long FileSize { get; set; }

        // Audit
        public DateTime UploadedOn { get; set; } = DateTime.UtcNow;

        // Soft delete
        public bool IsDeleted { get; set; } = false;
    }
}
