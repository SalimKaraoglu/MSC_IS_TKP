using ApplicationCore.Entities.PersonEntities.Abstract;

namespace IS_TKP.Models.ViewModels
{
    public class GetDepartmentVM
    {
        public int Id { get; set; }
        public string? DepartmentName { get; set; }
        public string? DepartmentDescription { get; set; }
        //public string? DepartmentManager { get; set; }
        public int? DepartmentSize { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Status Status { get; set; }
    }
}
