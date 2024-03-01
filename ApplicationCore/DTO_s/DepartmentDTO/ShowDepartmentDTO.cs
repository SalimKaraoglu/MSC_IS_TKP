using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DTO_s.DepartmentDTO
{
    public class ShowDepartmentDTO
    {
        public int Id { get; set; }
        public string? DepartmentName { get; set; }
        public string? DepartmentDescription { get; set; }
        public int? DepartmentSize { get; set; }
    }
}
