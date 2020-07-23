using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CiklumTest.Models.DBModels;
using CiklumTest.Models.ViewModels;
using CiklumTest.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CiklumTest.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	public class ToDoController : Controller
	{
		private readonly IToDoService toDoService;

		public ToDoController(IToDoService toDoService)
		{ 
			this.toDoService = toDoService;
		}

		[HttpGet("Get")]
		public async Task<IActionResult> Get(int id)
		{
			return Ok(await toDoService.Get(id));
		}

		[HttpPost("Add")]
		public async Task<IActionResult> Add([FromBody] CreateToDoVM data)
		{
			return Created("",await toDoService.Add(data));
		}

		[HttpPut("Edit")]
		public async Task<IActionResult> Edit([FromBody] ToDoVM data)
		{
			return Ok(await toDoService.Edit(data));
		}

		[HttpDelete("Remove")]
		public async Task<IActionResult> Remove(int id)
		{
			await toDoService.Remove(id);
			return Ok();
		}
	}
}
