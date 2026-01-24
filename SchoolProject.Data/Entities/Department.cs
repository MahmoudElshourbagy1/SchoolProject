using SchoolProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SchoolProject.Data._ُEntities
{
    public partial class Department
    {
        public Department() { 
        Students= new HashSet<Student>();
         DepartmetsSubjects = new HashSet<DepartmetSubject>();
        }
        [Key]
        public int DIO {  get; set; }
        [StringLength(200)]
        public string DName { get; set; }
        [InverseProperty("Departments")]
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<DepartmetSubject> DepartmetsSubjects { get; set; }
    }
}
