using DAL.Entities;

namespace BLL.Services.Interfaces
{
    public interface IRoleService
    {
        Task<List<Role>> GetRolesAsync();
    }
}
