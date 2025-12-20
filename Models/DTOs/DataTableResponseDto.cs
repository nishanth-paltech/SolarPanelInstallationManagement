namespace SolarPanelInstallationManagement.Models.DTOs
{
    public class DataTableResponseDto<T>
    {
        public int Draw { get; set; }
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
        public IEnumerable<T> Data { get; set; } = Enumerable.Empty<T>();
    }
}
