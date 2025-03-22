using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.Repositories.Implements;
using DAL.Repositories.Interfaces;

namespace BLL.Services.Implements
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository roleRepository;

        public RoleService()
        {
            roleRepository = new RoleRepository();
        }

        public async Task<List<Role>> GetRolesAsync()
        {
            return await roleRepository.GetRolesAsync();
        }
    }
}
