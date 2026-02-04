using SchoolProject.Data.Commens;
using System.ComponentModel.DataAnnotations;

namespace SchoolProject.Data.Entities.Views
{

    public class ViewDepartment : GeneralLocalizableEntity
    {
        [Key]
        public int DIO { get; set; }
        public string? DNameAr { get; set; }
        public string? DNameEn { get; set; }
        public int StudentCount { get; set; }
    }
}
