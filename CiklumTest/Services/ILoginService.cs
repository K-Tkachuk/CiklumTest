using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CiklumTest.Models.DBModels;
using CiklumTest.Models.Identyty;
using CiklumTest.Models.Settings;
using System.Text;
using CiklumTest.Helpers;
using CiklumTest.Enums;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;

namespace CiklumTest.Services
{
	public interface ILoginService
	{
		Task<ClaimsIdentity> GetIdentity(LoginRequest model);
		Task<LoginResponse> CreateToken(ClaimsIdentity identity);
		User GetUser();
	}

	public class LoginService : ILoginService
	{
		private readonly Settings _settings;
		private readonly CiklumDbContext _db;
		private readonly IHttpContextAccessor _httpContextAccessor;

		User user { get; set; }


        public LoginService(IOptions<Settings> settingsOptions, CiklumDbContext context, IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
			_settings = settingsOptions.Value;
			_db = context;

			var email = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
			if(email != null)
				user = _db.Users.FirstOrDefault(x => x.Email == email.Value);
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
				_settings.AuthOptions.Issuer,
				_settings.AuthOptions.Audience,
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

        public User GetUser()
        {
			return user;
        }
    }
}
