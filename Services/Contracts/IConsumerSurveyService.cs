using SolarPanelInstallationManagement.Models.DTOs;
using System.Threading.Tasks;
using SolarPanelInstallationManagement.Models.Entities;

namespace SolarPanelInstallationManagement.Services.Contracts
{
    public interface IConsumerSurveyService
    {
        Task<DataTableResponseDto<ConsumerSurvey>> GetDataTableAsync(DataTableRequestDto request);

        Task<ConsumerSurvey?> GetByIdAsync(int id);
        Task AddAsync(ConsumerSurvey survey);
        Task UpdateAsync(ConsumerSurvey survey);
        Task DeleteAsync(int id);
    }
}
