using DAL.Entities;

namespace DAL.Repositories.Interfaces
{
    public interface IServiceRepository
    {
        Task<List<Service>> GetServicesAsync(string serviceName);
        Task<bool> DisableServiceAsync(int serviceId);
    }
}
