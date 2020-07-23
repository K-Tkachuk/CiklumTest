using System;
using CiklumTest.Models.DBModels;
using CiklumTest.Repositories.Interfaces;

namespace CiklumTest.Repositories
{
    public class UserRepository : Repository<User> , IUserRepository
    {
        public UserRepository(CiklumDbContext context) : base(context)
        {
        }
    }
}
