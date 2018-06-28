using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CiklumTest.Models.DBModels;
using CiklumTest.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace CiklumTest.Services
{
    public interface IUserService
    {
		Task<IEnumerable<User>> Get();
        Task<int> Remove(int id);
		Task<int> Add(UserDTO items);
    }

	public class UserService : IUserService
    {
        readonly CiklumDbContext _db;

		public UserService(CiklumDbContext context)
        {
            _db = context;
        }
		public async Task<IEnumerable<User>> Get()
        {
            return await _db.Users.ToListAsync();
        }

		public async Task<int> Add(UserDTO item)
        {
            if (item is User obj)
            {
				_db.Users.Add(obj);            
            }

            return await _db.SaveChangesAsync();
        }
        
        public async Task<int> Remove(int id)
        {
			_db.Users.Remove(new User() { Id = id });
            return await _db.SaveChangesAsync();
        }      
	}
}
