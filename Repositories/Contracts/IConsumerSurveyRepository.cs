using SolarPanelInstallationManagement.Models.Entities;

namespace SolarPanelInstallationManagement.Repositories.Contracts
{
    public interface IConsumerSurveyRepository : IGenericRepository<ConsumerSurvey>
    {
        Task<ConsumerSurvey?> GetByServiceNumberAsync(string serviceNumber);

        IQueryable<ConsumerSurvey> GetForDataTable();
    }
}
