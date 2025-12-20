using Microsoft.EntityFrameworkCore;
using SolarPanelInstallationManagement.Data;
using SolarPanelInstallationManagement.Models.Entities;
using SolarPanelInstallationManagement.Repositories.Contracts;

namespace SolarPanelInstallationManagement.Repositories.Implementations
{
    public class ConsumerSurveyRepository
        : GenericRepository<ConsumerSurvey>, IConsumerSurveyRepository
    {
        public ConsumerSurveyRepository(AppDbContext context)
            : base(context)
        {
        }

        public async Task<ConsumerSurvey?> GetByServiceNumberAsync(string serviceNumber)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ServiceNumber == serviceNumber);
        }

        public IQueryable<ConsumerSurvey> GetForDataTable()
        {
            return _dbSet.AsNoTracking();
        }
    }
}
