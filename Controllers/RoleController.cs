using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Smart_Flower_Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> roleManger;
        public RoleController(RoleManager<IdentityRole> roleManger)
        {
            this.roleManger = roleManger;

        }



        [HttpPost("create-role")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
         
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return BadRequest("Role name cannot be empty.");
            }

       
            bool roleExists = await roleManger.RoleExistsAsync(roleName);
            if (roleExists)
            {
                return BadRequest($"The role '{roleName}' already exists.");
            }


            IdentityResult result = await roleManger.CreateAsync(new IdentityRole(roleName));

            if (result.Succeeded)
            {
                return Ok($"Role '{roleName}' created successfully.");
            }

            return BadRequest(result.Errors);
        }

        
     





    }
}
