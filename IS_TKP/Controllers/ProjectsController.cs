using AutoMapper;
using Infrastructure.Services.Interface;
using IS_TKP.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Entities.PersonEntities.Abstract;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Entities.PersonEntities.Concrete;

namespace IS_TKP.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IProjectRepository _projectRepo;
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepo;
        private readonly IDepartmentRepository _departmentRepo;

        public ProjectsController(IProjectRepository projectRepo, IMapper mapper, IEmployeeRepository employeeRepo, IDepartmentRepository departmentRepo)
        {
            _projectRepo = projectRepo;
            _mapper = mapper;
            _employeeRepo = employeeRepo;
            _departmentRepo = departmentRepo;
        }

        public async Task<IActionResult> Index()
        {
            var projects = await _projectRepo.GetFilteredListAsync
                (
                select: x => new GetProjectVM
                {
                    Id = x.Id,
                    ProjectName = x.ProjectName,
                    ProjectDescription = x.ProjectDescription,
                    StartingDate = x.StartingDate,
                    CreatedDate = x.CreatedDate,
                    UpdatedDate = x.UpdatedDate,
                    Status = x.Status,
                    ProjectManager = $"{x.Employees.Take(1).First().FirstName} {x.Employees.Take(1).First().LastName}",
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderByDescending(z => z.CreatedDate),
                join: x => x.Include(y => y.Employees).ThenInclude(z => z.Department)
                );
            
            return View(projects);
        }
    }
}
