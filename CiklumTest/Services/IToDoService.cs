using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CiklumTest.Enums;
using CiklumTest.Helpers;
using CiklumTest.Models.DBModels;
using CiklumTest.Models.Enums;
using CiklumTest.Models.ViewModels;
using CiklumTest.Repositories.Interfaces;

namespace CiklumTest.Services
{
    public interface IToDoService
    {
        Task<ToDoVM> Get(int id);
        Task<ToDoVM> Edit(ToDoVM item);
        Task Remove(int id);
        Task<ToDoVM> Add(CreateToDoVM item);
    }

    public class ToDoService : IToDoService
    {
		private readonly ILoginService loginService;
        private readonly IToDoRepository todoRepository;
        private readonly IMapper mapper;

        public ToDoService(
            ILoginService loginService,
            IToDoRepository todoRepository,
            IMapper mapper)
        {
            this.mapper = mapper;
            this.todoRepository = todoRepository;
            this.loginService = loginService;
        }
        public async Task<ToDoVM> Get(int id)
        {
            var todo = await todoRepository.GetById(id);

            return mapper.Map<ToDoVM>(todo);
        }
        
        public async Task<ToDoVM> Edit(ToDoVM item)
        {
            var toDo = await todoRepository.GetById(item.Id);
            if (toDo == null)
                throw new CiklumTestException(Errors.DataNotFound);

            toDo.Description = item.Description;
            toDo.Header = item.Header;
            toDo.State = item.State;

            todoRepository.Update(toDo);

            await todoRepository.Save();

            return await Task.FromResult(mapper.Map<ToDoVM>(toDo));
        }

        public async Task<ToDoVM> Add(CreateToDoVM item)
        {
            var toDo = mapper.Map<ToDo>(item);
           
            toDo.UserId = loginService.GetUser().Id;
            toDo.State = TaskState.New;
            await todoRepository.Create(toDo);

            await todoRepository.Save();

            return await Task.FromResult(mapper.Map<ToDoVM>(toDo));
        }

        public Task Remove(int id)
        {
            todoRepository.Delete(id);
			return todoRepository.Save();
        }
    }
}
