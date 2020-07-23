using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CiklumTest.Models.DBModels;

namespace CiklumTest.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
       
        Task<int> Count();
        int Count(Func<T, bool> predicate);
        Task Create(T entity);

        Task<List<T>> GetAll(int skip,int take);

        IEnumerable<T> Find(Func<T, bool> predicate);

        CiklumDbContext GetContext();

        Task<T> GetById(int id);

        void Update(T entity);
        void Delete(T entity);
        void Delete(int id);
        Task<int> Save();
    }
}
