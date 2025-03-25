using DAL.Entities;

namespace BLL.Services.Interfaces
{
    public interface IServiceService
    {
        Task<List<Service>> GetServicesAsync(string serviceName, string status);
        Task<bool> DisableServiceAsync(int serviceId);
    }
}
