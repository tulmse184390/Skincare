using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.Repositories.Implements;
using DAL.Repositories.Interfaces;
using System.Diagnostics;

namespace BLL.Services.Implements
{
    public class ServiceService : IServiceService {
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

        public bool AddService(Service service) {
            Debug.WriteLine(service.ServiceName);
            var tempService = serviceRepository.GetService(service.ServiceName);
            if (tempService != null) {
                return false;
            } else {
                serviceRepository.AddService(service);
                return true;
            }
        }

        public void UpdateService(Service service) {
            var tempService = serviceRepository.GetServiceById(service.ServiceId);
            if (tempService != null) {
                tempService.ServiceName = service.ServiceName;
                tempService.Duration = service.Duration;
                tempService.Price = service.Price;
                tempService.Status = service.Status;
                tempService.Description = service.Description;
                serviceRepository.UpdateSerive(tempService);
            }
        }

        public void DeleteService(int ServiceId) {
            var tempService = serviceRepository.GetServiceById(ServiceId);
            if (tempService != null) {
                serviceRepository.DeleteService(tempService);
            }
        }
    }
}
