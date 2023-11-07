using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IDepartmetRepository:IGenericRepository<Department>//Used To Inherit From Class IGenericRepository the 5 Methods
	{
        //IEnumerable<Department> GetAll();//Return Collection Of Department

        //Department GetById(int Id);

        //int Add(Department department);//int=>Beacuse To Return Number Of Rows Affected At Database

        //int Update(Department department);
        //int Delete(Department department);
    }
}
