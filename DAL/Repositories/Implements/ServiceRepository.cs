using DAL.Datas;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DAL.Repositories.Implements
{
    public class ServiceRepository : IServiceRepository {
        private readonly SkinCareContext dbContext;

        public ServiceRepository()
        {
            dbContext = new();
        }

        public async Task<List<Service>> GetServicesAsync(string serviceName)
        {
            return await dbContext.Services.Where(x => x.ServiceName.ToLower().Contains(serviceName.ToLower())).ToListAsync();
        }

        public List<Service> GetAllServices() {
            return dbContext.Services.ToList();
        }

        public async Task<bool> DisableServiceAsync(int serviceId)
        {
            var tmpService = await dbContext.Services.FirstOrDefaultAsync(x => x.ServiceId == serviceId);
            if (tmpService == null)
            {
                return false;
            }
            tmpService.Status = "Disable";
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task AddService(Service service) {
            dbContext.Services.Add(service);
            await dbContext.SaveChangesAsync();
        }

        public Service GetService(string serviceName) {
            Debug.WriteLine(serviceName);
            return dbContext.Services.FirstOrDefault(x => x.ServiceName.Equals(serviceName));
        }

        public Service GetServiceById(int serviceId) {
            return dbContext.Services.FirstOrDefault(x => x.ServiceId == serviceId);
        }

        public void UpdateSerive(Service service) {
            dbContext.Services.Update(service);
            dbContext.SaveChanges();
        }

        public void DeleteService(Service service) {
            dbContext.Services.Remove(service);
            dbContext.SaveChanges();
        }
    }
}
