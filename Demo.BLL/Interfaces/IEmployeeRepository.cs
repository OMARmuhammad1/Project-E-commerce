using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
	public interface IEmployeeRepository:IGenericRepository<Employee>//Used To Inherit From Class IGenericRepository the 5 Methods
	{
		//IEnumerable<Employee> GetAll();//Return Collection Of Department

		//Employee GetById(int Id);

		//int Add(Employee employee);//int=>Beacuse To Return Number Of Rows Affected At Database

		//int Update(Employee employee);
		//int Delete(Employee employee);

		IQueryable <Employee> GetEmployeeByAddress(string address);

		/*IEnumerable*/IQueryable<Employee> Search(string Name);
	}
}
