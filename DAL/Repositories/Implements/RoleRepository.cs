using DAL.Datas;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Implements
{
    public class RoleRepository : IRoleRepository
    {
        private readonly SkinCareContext dbContext;

        public RoleRepository()
        {
            dbContext = new();
        }

        public async Task<List<Role>> GetRolesAsync()
        {
            return await dbContext.Roles.ToListAsync();
        }
    }
}
