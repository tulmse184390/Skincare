using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.Repositories.Implements;
using DAL.Repositories.Interfaces;

namespace BLL.Services.Implements
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository serviceRepository;

        public ServiceService()
        {
            serviceRepository = new ServiceRepository();
        }

        public async Task<List<Service>> GetServicesAsync(string serviceName, string status)
        {
            var list = await serviceRepository.GetServicesAsync(serviceName);
            list = list.Where(x => x.Status.ToLower() == status.ToLower()).ToList();
            return list;
        }

        public List<Service> GetAllServices() {
            return serviceRepository.GetAllServices();
        }

        public Task<bool> DisableServiceAsync(int serviceId)
        {
            throw new NotImplementedException();
        }
    }
}
