using SchoolProject.Data.Commens;

namespace SchoolProject.Data.Entities
{

    public class DepartmentStudentCountProc : GeneralLocalizableEntity
    {

        public int DIO { get; set; }
        public string? DNameAr { get; set; }
        public string? DNameEn { get; set; }
        public int StudentCount { get; set; }
    }
    public class DepartmentStudentCountProcParameters
    {
        public int DIO { get; set; } = 0;
    }
}
