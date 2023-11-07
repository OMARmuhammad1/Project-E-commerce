using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		private readonly MVCAppDbContext _dbContext;

		public GenericRepository(MVCAppDbContext dbContext)
        {
			_dbContext = dbContext;
		}
        public async Task/*void*/ AddAsync(T item)
		{
			await _dbContext.AddAsync(item);
			//return _dbContext.SaveChanges();
		}
		 
		public void /*int*/ Delete(T item)
		{
			_dbContext.Remove(item);
			//return _dbContext.SaveChanges();
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			if(typeof(T)==typeof(Employee))//Eager Loading
			{
				return(IEnumerable<T>) await _dbContext.Employees.Include(E=>E.Department).ToListAsync();
			}
			return await _dbContext.Set<T>().ToListAsync();
		}

		public async Task<T> GetByIdAsync(int Id)
		{
			return await _dbContext.Set<T>().FindAsync(Id);
		}

		public void /*int*/ Update(T item)
		{
			_dbContext.Update(item);
			//return _dbContext.SaveChanges();
		}
	}
}
