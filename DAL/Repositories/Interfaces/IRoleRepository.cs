using DAL.Entities;

namespace DAL.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<List<Role>> GetRolesAsync();
    }
}
