using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
	public interface IUnitOfWork
	{
		//Signature For Property For Each And Every Repository Interface
		public IEmployeeRepository EmployeeRepository { get; set; }
		public IDepartmetRepository DepartmentRepository { get; set; }
		Task<int> CompleteAsync();
		//void Dispose();
	}
}
