using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using FinancesAPI.Data;
using FinancesAPI.Models.DTO;
using FinancesAPI.Models.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancesAPI.Controllers
{
    [ApiController]
    [Route("/user")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService) => 
                _userService = userService;

        [HttpPost]
        [Route("/user/")]
        public async Task<IActionResult> CreateNewUserAsync([FromBody] UserDTO userDTO)
        {
            var userToReturn = await _userService.CreteNewUserAsync(userDTO);
            return Created("/users/created", userToReturn);
        }

        [HttpGet]
        [Route("/user/{id}")]
        [Authorize]
        public async Task<IActionResult> ReturnUserAndWalletAsync(int id)
        {
            var contentToReturn = await _userService.ReturnUserInformation(id);
            return Ok(contentToReturn);
        }

        [HttpPost("/user/login")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel userToLogin)
        {
            var token = await _userService.GetLoginToken(userToLogin);
            if(token != null)
                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            else
                return Unauthorized();
        }
    }
}