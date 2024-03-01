using ApplicationCore.Entities.Interface;
using ApplicationCore.Entities.PersonEntities.Abstract;

namespace IS_TKP.Models.ViewModels
{
    public class GetEmployeeVM
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Status Status { get; set; }
    }
}
