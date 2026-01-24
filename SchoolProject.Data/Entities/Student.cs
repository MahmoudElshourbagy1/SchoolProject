using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SchoolProject.Data._ُEntities
{
    public class Student
    {
        [Key]
        public int StudID { get; set; }
        [StringLength(200)]
        public string Name { get; set; }
        [StringLength(200)]
        public string Address { get; set; }
        [StringLength(200)]
        public int Phone { get; set; }
        public int? DIO {  get; set; }
        [ForeignKey("DIO")]
        [InverseProperty("Students")]
        public virtual Department Departments { get; set; }
    }
}
