using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DTO_s.EmployeeDTO
{
    public class ShowEmployeeDTO
    {
        public int Id { get; set; }
        public string? EmployeeName { get; set; }
        public string? DeparmtmentName { get; set; }
        public int? DepartmentSize { get; set; }
    }
}
