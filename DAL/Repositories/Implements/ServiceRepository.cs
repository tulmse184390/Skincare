using DAL.Datas;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Implements
{
    public class ServiceRepository : IServiceRepository
    {
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
    }
}
