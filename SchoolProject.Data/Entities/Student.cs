using SchoolProject.Data.Commens;
using SchoolProject.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data._ُEntities
{
    public class Student : GeneralLocalizableEntity
    {
        public Student()
        {
            StudentSubjects = new HashSet<StudentSubject>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int StudID { get; set; }
        [StringLength(200)]
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public string? Address { get; set; }
        [StringLength(200)]
        public int? Phone { get; set; }
        public int? DIO { get; set; }
        [ForeignKey("DIO")]
        [InverseProperty("Students")]
        public virtual Department? Departments { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<StudentSubject> StudentSubjects { get; set; }
    }
}
