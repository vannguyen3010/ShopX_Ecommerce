using Shared;

namespace Contracts
{
    public interface IRoleRepository
    {
        Task<bool> AssignRoleAsync(string userId, RoleEnum role);
        Task<bool> RemoveRoleAsync(string userId, RoleEnum role);
    }
}
