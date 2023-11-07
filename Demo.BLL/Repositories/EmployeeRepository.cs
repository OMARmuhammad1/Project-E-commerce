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
	public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
	{
		private readonly MVCAppDbContext _dbContext;

		public EmployeeRepository(MVCAppDbContext dbContext):base(dbContext)//Constructor Chaining to constructor at base(GenericRepository)
		{
			_dbContext = dbContext;
		}
        public IQueryable<Employee> GetEmployeeByAddress(string address)
		{
			return _dbContext.Employees.Where(E=>E.Address==address);	
		}

        public IQueryable<Employee> Search(string Name)
        {
            return _dbContext.Employees.Where(E => E.Name.ToUpper().Contains(Name.ToUpper())).Include(E=>E.Department);
        }
        //private readonly MVCAppDbContext _dbContext;

        //public EmployeeRepository(MVCAppDbContext dbContext)//Ask clr for creating object from dbcontext
        //{
        //	_dbContext = dbContext;
        //}
        //      public int Add(Employee employee)
        //{
        //	_dbContext.Add(employee);
        //	return _dbContext.SaveChanges();
        //}

        //public int Delete(Employee employee)
        //{
        //	_dbContext.Remove(employee);
        //	return _dbContext.SaveChanges();	
        //}

        //public IEnumerable<Employee> GetAll()
        //{
        //	return _dbContext.Employees.ToList();
        //}

        //public Employee GetById(int Id)
        //{
        //	return _dbContext.Employees.Find(Id);
        //}

        //public int Update(Employee employee)
        //{
        //	_dbContext.Update(employee);
        //	return _dbContext.SaveChanges();	
        //}

    }
}
