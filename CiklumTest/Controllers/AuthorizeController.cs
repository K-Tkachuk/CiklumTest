using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CiklumTest.Models.Identyty;
using CiklumTest.Models.Settings;
using CiklumTest.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CiklumTest.Controllers
{
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class AuthorizeController : Controller
	{

		readonly Settings settings;
		readonly ILoginService loginService;

		public AuthorizeController(IOptions<Settings> settings, ILoginService loginService)
		{
			this.settings = settings.Value;
			this.loginService = loginService;
		}

		/// <summary>
		/// Авторизация пользователя по почте
		/// </summary>
		/// <returns></returns>
		[HttpPost("Auth/Email")]
		public async Task<IActionResult> Email([FromBody]LoginRequest model)
		{
			var identity = await loginService.GetIdentity(model);
			var token = await loginService.CreateToken(identity);
			return Ok(token);
		}
	}
}
