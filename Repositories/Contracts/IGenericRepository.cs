namespace SolarPanelInstallationManagement.Repositories.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> Query();

        Task<T?> GetByIdAsync(object id);

        Task AddAsync(T entity);

        void Update(T entity);

        void Delete(T entity);

        Task SaveChangesAsync();
    }
}
