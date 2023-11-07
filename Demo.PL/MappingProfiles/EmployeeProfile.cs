using AutoMapper;
using Demo.DAL.Models;
using Demo.PL.ViewModels;

namespace Demo.PL.MappingProfiles
{
	public class EmployeeProfile:Profile
	{
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
                /*.ForMember(d=>d.Name,O=>O.MapFrom(S=>S.EmpName));*///We use it if the property at employeeviewmodel is not match the same name at employee like{EmpName(AT employeeviewmodel),Name(At Employee)}
        }
    }
}
