using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models
{
	public class Employee //Must Be Public To Use It To Make DbSet To It To Convert It To Table At Database
	{
		public int Id { get; set; }//PK
		[Required/*(ErrorMessage ="Name Is Required")*/]//FrontEnd Validation
		[MaxLength(50/*,ErrorMessage ="Max Length Is 50 Chars"*/)]//FrontEnd Validation
		/*[MinLength(5,ErrorMessage = "Min Length Is 5 Chars")]*///FrontEnd Validation
		public string Name { get; set; }

		/*[Range(22,35,ErrorMessage ="Age Must Be In Range From 22 To 35")]*///FrontEnd Validation
		public int? Age { get; set; }

		//[RegularExpression("^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
		//	ErrorMessage ="Address Must Be Like 123-Street-City-Country")]//FrontEnd Validation
		public string Address { get; set; }
		//[DataType(DataType.Currency)] 
		public decimal Salary { get; set; }
		public bool IsActive { get; set; }
		//[EmailAddress]
		public string Email { get; set; }
		//[Phone]
		public string PhoneNumber { get; set; }

		public DateTime HireDate { get; set; }
		public DateTime CreationDate { get; set;}=DateTime.Now;//Will Take The Time That Will Create This New Record Now

		public string ImageName { get; set; }

		[ForeignKey("Department")]
		public int? DepartmentId { get; set; }//FK

		[InverseProperty("Employees")]
		public Department Department { get; set; }
	}
}
