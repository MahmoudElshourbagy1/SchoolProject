using SchoolProject.Data.Commens;
using SchoolProject.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data._ُEntities
{
    public partial class Department : GeneralLocalizableEntity
    {
        public Department()
        {
            Students = new HashSet<Student>();
            DepartmentsSubjects = new HashSet<DepartmentSubject>();
            Instructors = new HashSet<Instructor>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DIO { get; set; }
        [StringLength(200)]
        public string? DNameAr { get; set; }
        [StringLength(200)]
        public string? DNameEn { get; set; }
        public int? InsManager { get; set; }
        [StringLength(200)]
        [InverseProperty(nameof(Student.Departments))]
        public virtual ICollection<Student> Students { get; set; }
        [InverseProperty("Department")]
        public virtual ICollection<DepartmentSubject> DepartmentsSubjects { get; set; }
        [InverseProperty("department")]
        public virtual ICollection<Instructor> Instructors { get; set; }
        [ForeignKey("InsManager")]
        [InverseProperty("departmentManager")]
        public virtual Instructor? Instructor { get; set; }
    }
}
