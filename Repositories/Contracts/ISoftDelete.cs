namespace SolarPanelInstallationManagement.Repositories.Contracts
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}
