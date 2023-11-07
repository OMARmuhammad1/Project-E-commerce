using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>,IDepartmetRepository
    {
        public DepartmentRepository(MVCAppDbContext dbContext):base(dbContext)//Constructor Chaining to constructor at base(GenericRepository)
        {
            
        }
        //private readonly MVCAppDbContext _dbContext;

        ////private MVCAppDbContext _dbcontext;

        //public DepartmentRepository(MVCAppDbContext dbContext)//Ask clr for creating object from dbcontext
        //{
        //    _dbContext = dbContext;

        //}
        //public int Add(Department department)
        //{
        //    _dbContext.Add(department);
        //    return _dbContext.SaveChanges();
        //}

        //public int Delete(Department department)
        //{
        //    _dbContext.Remove(department);
        //    return _dbContext.SaveChanges();
        //}

        //public IEnumerable<Department> GetAll()
        //{
        //    return _dbContext.Departments.ToList();
        //}

        //public Department GetById(int Id)
        //{
        //    #region First Way
        //    //var department= _dbContext.Departments.Local.Where(D => D.Id == Id).FirstOrDefault(); 
        //    //if(department is null)
        //    //  department = _dbContext.Departments.Where(D => D.Id == Id).FirstOrDefault();

        //    //return department; 
        //    #endregion

        //    #region Second Way
        //    return _dbContext.Departments.Find(Id); 
        //    #endregion
        //}

        //public int Update(Department department)
        //{
        //   _dbContext.Update(department);
        //    return _dbContext.SaveChanges();
        //}
    }
}
