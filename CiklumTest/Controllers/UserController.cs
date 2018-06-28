using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CiklumTest.Models.DTO;
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
		readonly IUserService _userService;

		public UserController(IUserService userService)
        {
			_userService = userService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Get()
        {
			return Ok(await _userService.Get());
        }
        
        [HttpPost("[action]")]
        public async Task<IActionResult> Add([FromBody] UserDTO data)
        {
			return Ok(await _userService.Add(data));
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> Remove(int id)
        {
			return Ok(await _userService.Remove(id));
        }
    }
}
