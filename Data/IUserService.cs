using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using FinancesAPI.Models;
using FinancesAPI.Models.DTO;
using FinancesAPI.Models.ResponseModels;

namespace FinancesAPI.Data
{
    public interface IUserService
    {
        Task<User> ReturnUser(int id);
        Task<JwtSecurityToken> GetLoginToken(UserLoginModel user);
        Task<UserResponse> CreteNewUserAsync(UserDTO userDTO);
        Task<UserMyWalletResponse> ReturnUserInformation(int id);
    }
}