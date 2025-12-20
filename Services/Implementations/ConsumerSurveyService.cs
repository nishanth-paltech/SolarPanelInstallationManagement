using Microsoft.EntityFrameworkCore;
using SolarPanelInstallationManagement.Models.DTOs;
using System.Linq;
using SolarPanelInstallationManagement.Models.Entities;
using SolarPanelInstallationManagement.Repositories.Contracts;
using SolarPanelInstallationManagement.Services.Contracts;

namespace SolarPanelInstallationManagement.Services.Implementations
{
    public class ConsumerSurveyService : IConsumerSurveyService
    {
        private readonly IConsumerSurveyRepository _repository;

        public ConsumerSurveyService(IConsumerSurveyRepository repository)
        {
            _repository = repository;
        }

        public async Task<ConsumerSurvey?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(ConsumerSurvey survey)
        {
            await _repository.AddAsync(survey);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(ConsumerSurvey survey)
        {
            _repository.Update(survey);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return;

            _repository.Delete(entity);
            await _repository.SaveChangesAsync();
        }

        public async Task<DataTableResponseDto<ConsumerSurvey>> GetDataTableAsync(DataTableRequestDto request)
        {
            var query = _repository.GetForDataTable();

            // Global search
            if (!string.IsNullOrWhiteSpace(request.SearchValue))
            {
                var search = request.SearchValue.ToLower();

                query = query.Where(x =>
                    (x.District ?? "").ToLower().Contains(search) ||
                    (x.Mandal ?? "").ToLower().Contains(search) ||
                    (x.Village ?? "").ToLower().Contains(search) ||
                    (x.ConsumerNameWithSurname ?? "").ToLower().Contains(search) ||
                    (x.ServiceNumber ?? "").ToLower().Contains(search)
                );
            }

            // Column-specific filtering
            foreach (var filter in request.ColumnSearch)
            {
                query = filter.Key switch
                {
                    "district" => query.Where(x => x.District!.Contains(filter.Value!)),
                    "mandal" => query.Where(x => x.Mandal!.Contains(filter.Value!)),
                    "village" => query.Where(x => x.Village!.Contains(filter.Value!)),
                    "serviceNumber" => query.Where(x => x.ServiceNumber!.Contains(filter.Value!)),
                    _ => query
                };
            }

            var recordsTotal = await query.CountAsync();

            // Sorting
            query = request.SortColumn switch
            {
                "district" => request.SortDirection == "asc"
                    ? query.OrderBy(x => x.District)
                    : query.OrderByDescending(x => x.District),

                "mandal" => request.SortDirection == "asc"
                    ? query.OrderBy(x => x.Mandal)
                    : query.OrderByDescending(x => x.Mandal),

                "serviceNumber" => request.SortDirection == "asc"
                    ? query.OrderBy(x => x.ServiceNumber)
                    : query.OrderByDescending(x => x.ServiceNumber),

                _ => query.OrderByDescending(x => x.Sno)
            };

            // Paging
            var data = await query
                .Skip(request.Start)
                .Take(request.Length)
                .ToListAsync();

            return new DataTableResponseDto<ConsumerSurvey>
            {
                Draw = request.Draw,
                RecordsTotal = recordsTotal,
                RecordsFiltered = recordsTotal,
                Data = data
            };
        }
    }
}
