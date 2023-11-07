using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
		private readonly IUnitOfWork _unitOfWork;

		//private readonly IDepartmetRepository _departmentRepository;

		public DepartmentController(IUnitOfWork unitOfWork/*IDepartmetRepository departmetRepository*/)//Ask CLR for creating object from class IDepartmetRepository 
        {
            //_departmentRepository = departmetRepository;
			_unitOfWork = unitOfWork;
		}
        public async Task<IActionResult> Index()
        {
            var department=/*_departmentRepository*/await _unitOfWork.DepartmentRepository.GetAllAsync();
            return View(department);
        }
        [HttpGet]
        public IActionResult Create()
        {           
            return View();
        }

        [HttpPost]
		public async Task<IActionResult> Create(Department department)
		{
			if(ModelState.IsValid) //Server Side Validation
            {
                await _unitOfWork.DepartmentRepository.AddAsync(department);
                int Result = await _unitOfWork.CompleteAsync()/*.Result*/;
				//3.Temp Data=>Dictionary Object
				//Transfer Data From Action To Action
				if (Result > 0)
                {
					TempData["Message"] = "Department is Created";
				}
                
                return RedirectToAction(nameof(Index));
            }
            return View(department);
		}

        public async Task<IActionResult> Details(int? id,string ViewName="Details")
        {
            if (id is null)
                return BadRequest(); //Status Code 400
            var department = await _unitOfWork.DepartmentRepository.GetByIdAsync(id.Value);
            if(department is null)
            {
                return NotFound();
            }
            return View(ViewName,department);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
         
            return await Details(id,"Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]//To Stop Any Tool To Make differnt To Input Data Like Post Man
		public async Task<IActionResult> Edit(Department department,[FromRoute]int id)
		{
            if(id!=department.Id)
                return BadRequest();
			if (ModelState.IsValid) //Server Side Validation
			{
                try
                {
					_unitOfWork.DepartmentRepository.Update(department);
                    await _unitOfWork.CompleteAsync();
					return RedirectToAction(nameof(Index));
				}
                catch(System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
				
			}
			return View(department);
		}
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {            
            return await Details(id, "Delete");
        }

		[HttpPost]
		[ValidateAntiForgeryToken]//To Stop Any Tool To Make differnt To Input Data Like Post Man
		public async Task<IActionResult> Delete(Department department, [FromRoute] int id)
		{
            if (id != department.Id)
                return BadRequest();
            if (ModelState.IsValid) //Server Side Validation
            {
                try
                {
                    _unitOfWork.DepartmentRepository.Delete(department);
					await _unitOfWork.CompleteAsync();
					return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            return View(department);
            //if (ModelState.IsValid) //Server Side Validation
            //{
            //	_unitOfWork.DepartmentRepository.Delete(department);
            //	return RedirectToAction(nameof(Index));
            //}
            //return View(department);
        }
	}
}
