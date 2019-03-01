using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CiklumTest.Models.DBModels;
using CiklumTest.Models.DTO;
using CiklumTest.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CiklumTest.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	public class ToDoController : Controller
	{
		readonly IToDoService _toDoService;

		public ToDoController(IToDoService toDoService)
		{
			_toDoService = toDoService;
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> Get()
		{
			return Ok(await _toDoService.Get());
		}

		[HttpPost("[action]")]
		public async Task<IActionResult> Add([FromBody] ToDoDTO data)
		{
			return Ok(await _toDoService.Add(data));
		}

		[HttpPut("[action]")]
		public async Task<IActionResult> Edit([FromBody] ToDoDTO data)
		{
			return Ok(await _toDoService.Edit(data));
		}

		[HttpDelete("[action]")]
		public async Task<IActionResult> Remove(int id)
		{
			return Ok(await _toDoService.Remove(id));
		}
	}
}
