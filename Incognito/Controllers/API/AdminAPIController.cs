using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Incognito.Controllers.API
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/v1/admin")]
    public class AdminAPIController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminAPIController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            if (id == null) return BadRequest();

            var role = await _roleManager.FindByIdAsync(id);

            if (role == null) return BadRequest();

            var result = await _roleManager.DeleteAsync(role);

            if (result.Succeeded) return Ok();

            return BadRequest();
        }
    }
}
