using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CiklumTest.Models.DBModels;
using CiklumTest.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace CiklumTest.Services
{
    public interface IToDoService
    {
        Task<IEnumerable<ToDoDTO>> Get();
        Task<int> Edit(ToDoDTO item);
        Task<int> Remove(int id);
        Task<int> Add(ToDoDTO items);
    }

    public class ToDoService : IToDoService
    {
        readonly CiklumDbContext _db;
		readonly LoginService _loginService;

		public ToDoService(CiklumDbContext context,LoginService loginService)
        {
            _db = context;
			_loginService = loginService;
        }
        public async Task<IEnumerable<ToDoDTO>> Get()
        {
			return await _db.ToDos
				            .Where(t=>t.UserId == _loginService.user.Id)
				            .Select(t => new ToDoDTO()
            {
                Id = t.Id,
                Header = t.Header,
                Description = t.Description,
                UserId = t.UserId
            }).ToListAsync();
        }
        
        public Task<int> Edit(ToDoDTO item)
        {
			var toDo = _db.ToDos
			              .Where(t => t.Id == item.Id && t.UserId == _loginService.user.Id)
			              .FirstOrDefault();
			
            if (toDo != null)
            {
                toDo.Description = item.Description;
                toDo.Header = item.Header;
                toDo.UserId = item.UserId;
            }
			return _db.SaveChangesAsync();
        }

        public async Task<int> Add(ToDoDTO item)
        {         
			if (item is ToDo obj)
			{
				obj.UserId = _loginService.user.Id;
				_db.ToDos.Add(obj);

			}

			return await _db.SaveChangesAsync();
        }

        public async Task<int> Remove(int id)
        {
			_db.ToDos.Remove(new ToDo() { Id = id ,UserId = _loginService.user.Id  });
			return await _db.SaveChangesAsync();
        }
    }
}
