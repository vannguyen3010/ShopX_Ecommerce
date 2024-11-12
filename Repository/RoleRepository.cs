using Contracts;
using Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Shared;

namespace Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<UserRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public RoleRepository(RoleManager<UserRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<bool> AssignRoleAsync(string userId, RoleEnum role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return false;
            }

            var roleName = role.ToString();
            if(!await _roleManager.RoleExistsAsync(roleName))
            {
                return false;
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);
            return result.Succeeded;
        }

        public async Task<bool> RemoveRoleAsync(string userId, RoleEnum role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            var roleName = role.ToString();
            var result = await _userManager.RemoveFromRoleAsync(user, roleName);
            return result.Succeeded;
        }
    }
}
