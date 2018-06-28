using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CiklumTest.Models.DBModels;
using CiklumTest.Models.Identyty;
using CiklumTest.Models.Settings;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CiklumTest.Helpers;
using CiklumTest.Enums;

namespace CiklumTest.Services
{
    public interface ILoginService
    {
        Task<ClaimsIdentity> GetIdentity(LoginRequest model);
        Task<LoginResponse> CreateToken(ClaimsIdentity identity);
    }
    
    public class LoginService : ILoginService
    {
        readonly Settings _settings;
        readonly CiklumDbContext _db;
		public User user { get; private set; }

        public LoginService(IOptions<Settings> settingsOptions, CiklumDbContext context)
        {
            _settings = settingsOptions.Value;
            _db = context;
        }

        public async Task<ClaimsIdentity> GetIdentity(LoginRequest model)
        {
            user = _db.Users.FirstOrDefault(x => x.Email == model.Email && x.Password == model.Password);
            if (user == null) throw new CiklumTestException(Errors.IncorrectEmailOrPassword);
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(_settings.AuthOptions.LifeTime)).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, model.Email),
                new Claim(JwtRegisteredClaimNames.Jti, model.Email)
            };

			return new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }

        public async Task<LoginResponse> CreateToken(ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;
            var credentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.AuthOptions.Key)), SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
                issuer: _settings.AuthOptions.Issuer,
                audience: _settings.AuthOptions.Audience,
                notBefore: now,
                claims: identity.Claims,
                expires: now.AddDays(_settings.AuthOptions.LifeTime),
                signingCredentials: credentials
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new LoginResponse
            {
                access_token = encodedJwt,
                username = identity.Name
            };
        }
    }
}
