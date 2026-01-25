using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SchoolProject.Core.Features.Students.Queries.Resuilts
{
    public class GetStudentListRes
    {
       
        public int StudID { get; set; }
       
        public string? Name { get; set; }
       
        public string? Address { get; set; }
      
        public string? DepartmentName { get; set; }
    }
}
