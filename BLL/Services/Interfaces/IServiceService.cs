using DAL.Entities;

namespace BLL.Services.Interfaces
{
    public interface IServiceService
    {
        Task<List<Service>> GetServicesAsync(string serviceName);
        Task<bool> DisableServiceAsync(int serviceId);
    }
}
