using System;
using AutoMapper;
using CiklumTest.Models.DBModels;
using CiklumTest.Models.ViewModels;

namespace CiklumTest.Models.Settings
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<User, UserVM>();
            CreateMap<UserVM, User>();

            CreateMap<ToDo, ToDoVM>();
            CreateMap<ToDoVM, ToDo>();

            CreateMap<CreateUserVM, User>();
            CreateMap<User, CreateUserVM>();

            CreateMap<CreateToDoVM, ToDo>();
        }
    }
}
