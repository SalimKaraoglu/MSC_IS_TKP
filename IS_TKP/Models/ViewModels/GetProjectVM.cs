using ApplicationCore.Entities.PersonEntities.Abstract;

namespace IS_TKP.Models.ViewModels
{
    public class GetProjectVM
    {
        public int Id { get; set; }
        public string? ProjectName { get; set; }
        public string? ProjectManager { get; set; }
        public string? ProjectDescription { get; set; }
        public DateTime? StartingDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Status Status { get; set; }
       

    }
}
