using AutoMapper;
using Infrastructure.Services.Interface;
using IS_TKP.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Entities.PersonEntities.Abstract;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.DTO_s.EmployeeDTO;
using ApplicationCore.Entities.PersonEntities.Concrete;
using ApplicationCore.Entities.PersonEntities.Abstract;
using ApplicationCore.DTO_s.DepartmentDTO;


namespace IS_TKP.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepository _employeeRepo;
        private readonly IMapper _mapper;
        private readonly IDepartmentRepository _departmentRepo;
        private readonly IProjectRepository _projectRepo;

        public EmployeesController(IEmployeeRepository employeeRepo, IMapper mapper, IDepartmentRepository departmentRepo)
        {
            _employeeRepo = employeeRepo;
            _mapper = mapper;
            _departmentRepo = departmentRepo;
        }

        public async Task<IActionResult> Index()
        {
            var employees = await _employeeRepo.GetFilteredListAsync
                (
                select: x => new GetEmployeeVM
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    HireDate = x.HireDate,
                    Email = x.Email,
                    CreatedDate = x.CreatedDate,
                    UpdatedDate = x.UpdatedDate,
                    Status = x.Status
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderByDescending(z => z.CreatedDate)
                );
            return View(employees);
        }

        public async Task<IActionResult> CreateEmployee()
        {
            var departments = await _departmentRepo.GetFilteredListAsync
             (
                    select: x => new ShowDepartmentDTO
                    {
                        Id = x.Id,
                        DepartmentName = x.DepartmentName,
                        DepartmentDescription = x.DepartmentDescription,
                        DepartmentSize = x.Employees.Where(x => x.Status != Status.Passive).ToList().Count,
                    },
                    where: x => x.Status != Status.Passive,
                    join: x => x.Include(z => z.Employees)
             );

            var model = new CreateEmployeeDTO
            {
                Departments = departments
            };
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeDTO model)
        {
            var departments = await _departmentRepo.GetFilteredListAsync
           (
                  select: x => new ShowDepartmentDTO
                  {
                      Id = x.Id,
                      DepartmentName = x.DepartmentName,
                      DepartmentDescription = x.DepartmentDescription,
                      DepartmentSize = x.Employees.Where(x => x.Status != Status.Passive).ToList().Count,
                  },
                  where: x => x.Status != Status.Passive,
                  join: x => x.Include(z => z.Employees)
           );
            model.Departments = departments;

            if (ModelState.IsValid)
            {
                var employee = _mapper.Map<Employee>(model);
                await _employeeRepo.AddAsync(employee);
                TempData["Success"] = $"{employee.FirstName}{employee.LastName} Personeli Sisteme Kaydedilmiştir!";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Lütfen Aşağıdaki Kurallara Uyunuz!";
            return View(model);
        }

        public async Task<IActionResult> UpdateEmployee(int id)
        {
            if (id > 0)
            {
                var employee = await _employeeRepo.GetByIdAsync(id);
                if (employee is not null)
                {
                    var model = _mapper.Map<UpdateEmployeeDTO>(employee);
                    var departments = await _departmentRepo.GetFilteredListAsync
            (
                select: x => new ShowDepartmentDTO
                {
                    Id = x.Id,
                    DepartmentName = x.DepartmentName,
                    DepartmentDescription = x.DepartmentDescription,
                    DepartmentSize = x.Employees.Where(x => x.Status != Status.Passive).ToList().Count,
                },
                where: x => x.Status != Status.Passive,
                join: x => x.Include(z => z.Employees)
            );
                    model.Departments = departments;
                    return View(model);
                }
            }
            TempData["Error"] = "Personel Bulunamadı!";
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateEmployee(UpdateEmployeeDTO model)
        {
            var departments = await _departmentRepo.GetFilteredListAsync
            (
                select: x => new ShowDepartmentDTO
                {
                    Id = x.Id,
                    DepartmentName = x.DepartmentName,
                    DepartmentDescription = x.DepartmentDescription,
                    DepartmentSize = x.Employees.Where(x => x.Status != Status.Passive).ToList().Count,
                },
                where: x => x.Status != Status.Passive,
                join: x => x.Include(z => z.Employees)
            );
            model.Departments = departments;

            if (ModelState.IsValid)
            {
                var employee = _mapper.Map<Employee>(model);
                await _employeeRepo.UpdateAsync(employee);
                TempData["Success"] = $"{employee.FirstName}{employee.LastName} Personeli güncellenmiştir!";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Lütfen Aşağıdaki kurallara uyunuz!";
            return View(model);
        }

        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (id > 0)
            {
                var employee = await _employeeRepo.GetByIdAsync(id);
                if (employee is not null)
                {
                    if (!await _employeeRepo.HasDepartment(id))
                    {
                        await _employeeRepo.DeleteAsync(employee);
                        TempData["Success"] = $"{employee.FirstName}{employee.LastName} Personel Kaydı Silinmiştir!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        var departmentList = await _employeeRepo.DepartmentNames(employee.Id);
                        TempData["Error"] = $"{employee.FirstName}{employee.LastName} Personel {departmentList} Departmanında Kayıtlıdır => ";
                        return RedirectToAction("Index");
                    }
                }
            }
            TempData["Error"] = "Personel Bulunamadı!";
            return RedirectToAction("Index");
        }


    }
}
