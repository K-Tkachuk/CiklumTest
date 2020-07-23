using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CiklumTest.Models.DBModels;
using CiklumTest.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CiklumTest.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly CiklumDbContext _context;

        public Repository(CiklumDbContext context)
        {
            _context = context;
        }

        public CiklumDbContext GetContext() => _context;

        public Task<int> Count()
        {
           return _context.Set<T>().CountAsync();
        }

        public int Count(Func<T, bool> predicate)
        {
            return _context.Set<T>().Count(predicate);
        }

        public Task Create(T entity)
        {
            return _context.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
        }

        public async void Delete(int id)
        {
             Delete(await GetById(id));
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public Task<List<T>> GetAll(int skip, int take)
        {
            return _context.Set<T>().Skip(skip).Take(take).ToListAsync();
        }

        public Task<T> GetById(int id)
        {
            return _context.Set<T>().FindAsync(id);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public Task<int> Save()
        {
            return  _context.SaveChangesAsync();
        }
    }
}