using SchoolProject.Data._ُEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{

    public class DepartmentSubject
    {
        [Key]
        public int DIO { get; set; }
        [Key]
        public int SubID { get; set; }

        [ForeignKey("DIO")]
        [InverseProperty("DepartmentsSubjects")]
        public virtual Department? Department { get; set; }

        [ForeignKey("SubID")]
        public virtual Subjects? Subjects { get; set; }
    }
}
