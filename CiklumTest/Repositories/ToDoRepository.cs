using System;
using CiklumTest.Models.DBModels;
using CiklumTest.Repositories;
using CiklumTest.Repositories.Interfaces;

namespace CiklumTest.Repositories
{
    public class ToDoRepository : Repository<ToDo>, IToDoRepository
    {
        public ToDoRepository(CiklumDbContext context) : base(context)
        {
        }
    }
}
