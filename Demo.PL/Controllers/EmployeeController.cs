using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Models;
using Demo.PL.Helpers;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    [Authorize]
	public class EmployeeController : Controller
	{
		//private readonly IEmployeeRepository _employeeRepository;
        //private readonly IDepartmetRepository _departmetRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public EmployeeController(/*IEmployeeRepository employeeRepository
            ,IDepartmetRepository departmetRepository*/IUnitOfWork unitOfWork 
            ,IMapper mapper)//Ask CLR for creating object from class IDepartmetRepository
		{
			//_employeeRepository = employeeRepository;
            // _departmetRepository = departmetRepository;
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
        public async Task<IActionResult> Index(string search ="")
		{
            
            if (string.IsNullOrEmpty(search))
            {
                var employees = /*_employeeRepository*/await _unitOfWork.EmployeeRepository.GetAllAsync();
                var MappedEmployee = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
                return View(MappedEmployee );
            }
            else
            { 
                var employees = _unitOfWork.EmployeeRepository/*_employeeRepository*/.Search(search);
                return View(employees);
            }


        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Departments=/*_departmetRepository*/await _unitOfWork.DepartmentRepository.GetAllAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid) 
            {

                employeeVM.ImageName= DocumentSettings.UploadFile(employeeVM.Image, "Images");


                var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                 await _unitOfWork.EmployeeRepository.AddAsync(MappedEmployee);
				int Result =await _unitOfWork.CompleteAsync();
                if (Result > 0)
                {
                    TempData["Message"] = "Employee is Added";
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employeeVM);
        }

        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
			ViewBag.Departments =await _unitOfWork.DepartmentRepository.GetAllAsync();
			if (id is null)
                return BadRequest(); 
            var Employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(id.Value);
            if (Employee is null)
            {
                return NotFound();
            }
            var MappedEmployee=_mapper.Map<Employee, EmployeeViewModel>(Employee);
            return View(ViewName, MappedEmployee);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
			ViewBag.Departments = _unitOfWork.DepartmentRepository.GetAllAsync();
			return await Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeViewModel employeeVM, [FromRoute] int id)
        {
            if (id != employeeVM.Id)
                return BadRequest();
            if (ModelState.IsValid) 
            {
                try
                {
                    if(employeeVM.Image is not null)
                    {
						employeeVM.ImageName = DocumentSettings.UploadFile(employeeVM.Image, "Images");
					}             
					var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);                   
                    _unitOfWork.EmployeeRepository.Update(MappedEmployee);
                    await _unitOfWork.CompleteAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            return View(employeeVM);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
			ViewBag.Departments = _unitOfWork.DepartmentRepository.GetAllAsync();
			return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(EmployeeViewModel employeeVM, [FromRoute] int id)
        {
            if (id != employeeVM.Id)
                return BadRequest();
			try
			{
				var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
				_unitOfWork.EmployeeRepository.Delete(MappedEmployee);
				var Result = await _unitOfWork.CompleteAsync();
				if (Result > 0 && employeeVM.ImageName is not null)
				{
					DocumentSettings.DeleteFile(employeeVM.ImageName, "Images");
				}
				return RedirectToAction(nameof(Index));
			}
			catch (System.Exception ex)
			{
				ModelState.AddModelError(string.Empty, ex.Message);
				return View(employeeVM);
			}
            
        }
    }
}
