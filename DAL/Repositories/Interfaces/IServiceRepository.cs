using DAL.Entities;

namespace DAL.Repositories.Interfaces
{
    public interface IServiceRepository
    {
        Task<List<Service>> GetServicesAsync(string serviceName);
        Task<bool> DisableServiceAsync(int serviceId);
        List<Service> GetAllServices();
        Task AddService(Service service);
        Service GetService(string stringName);
        Service GetServiceById(int serviceId);
        void UpdateSerive(Service service);
        void DeleteService(Service service);
    }
}
