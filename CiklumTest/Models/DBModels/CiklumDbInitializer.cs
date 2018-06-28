using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CiklumTest.Models.DBModels
{
    public static class CiklumDbInitializer
    {
        public static void Initialize(CiklumDbContext context)
        {
            context.Database.EnsureCreated();

            var userList = new List<User>()
                {
                    new User(){ Id = 1, Name = "Luke", Email= "test@gmail.com", Password = "123456" },
                    new User(){ Id = 2, Name = "Mike" ,Email= "test@mail.com", Password = "12345678"},
                    new User(){ Id = 3, Name = "Joe" ,Email= "test@yandex.com", Password = "testpas"},
                };

            context.Users.AddRange(userList);

            var toDoList = new List<ToDo>()
                {
                    new ToDo(){ Id = 1, Header="Task 1", Description="Task description 1", UserId = 1 },
                    new ToDo(){ Id = 2, Header="Task 2", Description="Task description 2", UserId = 1 },
                    new ToDo(){ Id = 3, Header="Task 3", Description="Task description 3", UserId = 1 },
                    new ToDo(){ Id = 4, Header="Task 4", Description="Task description 4", UserId = 1 },
                    new ToDo(){ Id = 5, Header="Task 5", Description="Task description 5", UserId = 2 },
                    new ToDo(){ Id = 6, Header="Task 6", Description="Task description 6", UserId = 2 },
                    new ToDo(){ Id = 7, Header="Task 7", Description="Task description 7", UserId = 3 },
                    new ToDo(){ Id = 8, Header="Task 8", Description="Task description 8", UserId = 3 },
                    new ToDo(){ Id = 9, Header="Task 9", Description="Task description 9", UserId = 3 }
                };

            context.ToDos.AddRange(toDoList);
            context.SaveChanges();
        }
    }
}