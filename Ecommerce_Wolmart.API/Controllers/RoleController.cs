using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace Ecommerce_Wolmart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;
        private readonly ILoggerManager _logger;

        public RoleController(IRoleRepository roleRepository, ILoggerManager logger)
        {
            _roleRepository = roleRepository;
            _logger = logger;
        }

        [HttpPost]
        [Route("assign-role")]
        public async Task<IActionResult> AssignRole(string userId, string role)
        {
            if (!Enum.TryParse(role, out RoleEnum parsedRole))
                return BadRequest("Invalid role");

            var result = await _roleRepository.AssignRoleAsync(userId, parsedRole);
            if (!result) return BadRequest("Failed to assign role");

            return Ok(result);
        }

        [HttpDelete]
        [Route("remove-role")]
        public async Task<IActionResult> RemoveRole(string userId, string role)
        {
            if (!Enum.TryParse(role, out RoleEnum parsedRole))
                return BadRequest("Invalid role");

            var result = await _roleRepository.RemoveRoleAsync(userId, parsedRole);
            if (!result) return BadRequest("Failed to remove role");

            return Ok("Role removed successfully");
        }
    }
}
