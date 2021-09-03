using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FinancesAPI.Data;
using FinancesAPI.Models;
using FinancesAPI.Models.DTO;
using FinancesAPI.Models.Exceptions;
using FinancesAPI.Models.ResponseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FinancesAPI.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context) => 
                _context = context;

        public async Task<User> ReturnUser(int id)
        {
            return await _context.Users.Include(x => x.MyWallet).Include(x => x.MyWallet.CurrencyInvestiments).FirstAsync(x => x.Id == id);
        }

        public async Task<JwtSecurityToken> GetLoginToken(UserLoginModel userToLogin)
        {
            var userToAuthenticate = await _context.Users.FirstAsync(x => x.Email == userToLogin.Email);
            if(userToAuthenticate.Email == null)
                throw new NotFoundException("User not found");
            if(userToAuthenticate.Password.Equals(userToLogin.Password))
            {
                string secutiryKey = "finances_api_gft_currency_exchange";
                var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secutiryKey));
                var credentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256Signature);
                var userClaims = new List<Claim>();
                userClaims.Add(new Claim("id", userToAuthenticate.Id.ToString()));
                var JWT = new JwtSecurityToken(
                        issuer: "FinancesAPI",
                        expires: DateTime.UtcNow.AddMinutes(25) ,
                        audience: "Beginner_investors",
                        signingCredentials: credentials,
                        claims: userClaims
                );
                return JWT;
            }
            return null;
        }

        public async Task<UserResponse> CreteNewUserAsync(UserDTO userDTO)
        {
            var userToSave = new User { 
                    Name = userDTO.Name,
                    Email = userDTO.Email,
                    Password = userDTO.Password,
                    MyWallet = new Wallet { NativeCurrency = userDTO.InitialDeposit }
             };
            _context.Users.Add(userToSave);
            await _context.SaveChangesAsync();
            return new UserResponse { Name = userDTO.Name, Email = userDTO.Email };
        }

        public async Task<UserMyWalletResponse> ReturnUserInformation(int id)
        {
            var user = await _context.Users
                            .Include(x => x.MyWallet)
                            .Include(x => x.MyWallet.CurrencyInvestiments)
                            .Include(x => x.MyWallet.CurrencyInvestiments)
                            .FirstOrDefaultAsync(x => x.Id == id);
            foreach (var item in user.MyWallet.CurrencyInvestiments)
                item.Value = Math.Round(item.Value, 4);
            var userAndWalletToReturn =  new UserMyWalletResponse {
                            Name = user.Name,
                            NativeCurrency = Math.Round(user.MyWallet.NativeCurrency, 4),
                            CurrencyInvestiments = user.MyWallet.CurrencyInvestiments
            };
            return userAndWalletToReturn;
        }
    }
}