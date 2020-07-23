using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CiklumTest.Enums;
using CiklumTest.Helpers;
using CiklumTest.Models.DBModels;
using CiklumTest.Models.ViewModels;
using CiklumTest.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CiklumTest.Services
{
	public interface IUserService
	{
		Task<UserVM> Get(int id);
		Task<UserVM> Add(CreateUserVM item);
		Task Remove(int id);
	}

	public class UserService : IUserService
	{
		private readonly IUserRepository userRepository;
		private readonly IMapper mapper;

		public UserService(IUserRepository userRepository,IMapper mapper)
		{
			this.mapper = mapper;
			this.userRepository = userRepository;
		}
		public async Task<UserVM> Get(int id)
		{
			return mapper.Map<UserVM>(await userRepository.GetById(id));
		}

		public async Task<UserVM> Add(CreateUserVM item)
		{
			User user = null;
            try
            {
				 user = userRepository
							.Find(u => u.Email == item.Email).FirstOrDefault();
			}
            catch(Exception ex)
            {
				var a = ex.Message;
            }

			if (user != null)
				throw new CiklumTestException(Errors.UserExist);

			user = mapper.Map<User>(item);

			await userRepository.Create(user);
			await userRepository.Save();

			return await Task.FromResult(mapper.Map<UserVM>(user));
		}

        public Task Remove(int id)
        {
			userRepository.Delete(id);
			return userRepository.Save();
		}
    }
}
