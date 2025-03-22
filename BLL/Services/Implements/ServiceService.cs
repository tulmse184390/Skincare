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

        public async Task<List<Service>> GetServicesAsync(string serviceName)
        {
            return await serviceRepository.GetServicesAsync(serviceName);
        }

        public Task<bool> DisableServiceAsync(int serviceId)
        {
            throw new NotImplementedException();
        }
    }
}
