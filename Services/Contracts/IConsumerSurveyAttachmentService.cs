using SolarPanelInstallationManagement.Models.Entities;

namespace SolarPanelInstallationManagement.Services.Contracts
{
    public interface IConsumerSurveyAttachmentService
    {
        Task<IEnumerable<ConsumerSurveyAttachment>> GetBySurveySnoAsync(int surveySno);

        Task UploadAsync(
            int surveySno,
            IEnumerable<IFormFile> files,
            CancellationToken cancellationToken = default);

        Task<ConsumerSurveyAttachment?> GetByIdAsync(int id);

        Task DeleteAsync(int id);
    }
}
