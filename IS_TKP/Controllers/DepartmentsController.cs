using AutoMapper;
using Infrastructure.Services.Interface;
using IS_TKP.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Entities.PersonEntities.Abstract;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Entities.PersonEntities.Concrete;
using ApplicationCore.DTO_s.DepartmentDTO;

namespace IS_TKP.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentRepository _departmentRepo;
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepo;

        public DepartmentsController(IDepartmentRepository departmentRepo, IMapper mapper, IEmployeeRepository employeeRepo)
        {
            _departmentRepo = departmentRepo;
            _mapper = mapper;
            _employeeRepo = employeeRepo;
        }


        public async Task<IActionResult> Index()
        {
            var departments = await _departmentRepo.GetFilteredListAsync
                (
                select: x => new GetDepartmentVM
                {
                    Id = x.Id,
                    DepartmentName = x.DepartmentName,
                    DepartmentDescription = x.DepartmentDescription,
                    DepartmentSize = x.Employees.Where(x => x.Status != Status.Passive).ToList().Count,
                    CreatedDate = x.CreatedDate,
                    UpdatedDate = x.UpdatedDate,
                    Status = x.Status
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderByDescending(z => z.CreatedDate),
                join: x => x.Include(z => z.Employees)
                 );

            return View(departments);
        }

        //var departments = await _departmentRepo.GetFilteredListAsync
        //    (
        //    select: x => new ShowDepartmentDTO
        //    {
        //        Id = x.Id,
        //        DepartmentName = x.DepartmentName,
        //        DepartmentDescription = x.DepartmentDescription,
        //        DepartmentSize  = x.Employees.Where(x => x.Status != Status.Passive).ToList().Count,

        //    },
        //    where: x => x.Status != Status.Passive,
        //    join: x => x.Include(z => z.Employees)
        //    );

        public async Task<IActionResult> CreateDepartment() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDepartment(CreateDepartmentDTO model)
        {

            if (ModelState.IsValid)
            {
                if (await _departmentRepo.AnyAsync(x =>
                                                       x.Status != Status.Passive &&
                                                       x.DepartmentName == model.DepartmentName))
                {
                    TempData["Error"] = "Başka bir Departman Adı Giriniz!";
                    return View(model);
                }
                else
                {
                    var department = _mapper.Map<Department>(model);
                    await _departmentRepo.AddAsync(department);
                    TempData["Success"] = $"{department.DepartmentName} Departmanı kaydedilmiştir.";
                    return RedirectToAction("Index");
                }
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }


        public async Task<IActionResult> UpdateDepartment(int id)
        {
            if (id > 0)
            {
                var department = await _departmentRepo.GetByIdAsync(id);

                if (department != null)
                {
                    var model = _mapper.Map<UpdateDepartmentDTO>(department);
                    return View(model);
                }
            }
            TempData["Error"] = "Departman Bulunamadı";
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateDepartment(UpdateDepartmentDTO model)
        {
                            
            if (ModelState.IsValid)
            {
                if (await _departmentRepo.AnyAsync(x =>
                                                   x.Status != Status.Passive && 
                                                   (x.Id != model.Id && x.DepartmentName == model.DepartmentName)))
                {
                    TempData["Error"] = "Lütfen Başka bir sınıf adı giriniz!";
                    return View(model);
                }
                else
                {
                    var department = _mapper.Map<Department>(model);
                    await _departmentRepo.UpdateAsync(department);
                    TempData["Success"] = $"{department.DepartmentName} Departmanı Güncellenmiştir!";
                    return RedirectToAction("Index");
                }
               

            }
            TempData["Error"] = "Lütfen Aşağıdaki Kurallara Uyunuz!";
            return View(model);
        }

        public async Task<IActionResult> DeleteDepartment(int id)
        {
            if (id > 0)
            {
                var deparment = await _departmentRepo.GetByIdAsync(id);
                {
                    if (deparment != null)
                    {
                        await _departmentRepo.DeleteAsync(deparment);
                        TempData["Success"] = $"{deparment.DepartmentName} Departmanı Silinmiştir!";
                        return RedirectToAction("Index");
                    }
                }
            }
            TempData["Error"] = "Deparman Bulunamadı!";
            return RedirectToAction("Index");
        }
    }
}
