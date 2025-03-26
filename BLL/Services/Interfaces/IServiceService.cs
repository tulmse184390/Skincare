using DAL.Entities;

namespace BLL.Services.Interfaces
{
    public interface IServiceService
    {
        Task<List<Service>> GetServicesAsync(string serviceName, string status);
        Task<bool> DisableServiceAsync(int serviceId);
        List<Service> GetAllServices();
        bool AddService(Service service);
        void UpdateService(Service service);
        void DeleteService(int ServiceId);
    }
}
