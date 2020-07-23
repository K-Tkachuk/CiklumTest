using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CiklumTest.Models.ViewModels;
using CiklumTest.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CiklumTest.Controllers
{
	[Authorize]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService userService;

		public UserController(IUserService userService)
        {
			this.userService = userService;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get(int id)
        {
			return Ok(await userService.Get(id));
        }
        
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] CreateUserVM data)
        {
            return Created("", await userService.Add(data));
        }

        [HttpDelete("Remove")]
        public async Task<IActionResult> Remove(int id)
        {
            await userService.Remove(id);
            return Ok();
        }
    }
}
