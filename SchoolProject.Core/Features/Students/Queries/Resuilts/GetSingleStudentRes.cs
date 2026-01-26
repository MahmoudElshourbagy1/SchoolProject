using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.Core.Features.Students.Queries.Resuilts
{
    public class GetSingleStudentRes
    {
        public int StudID { get; set; }

        public string? Name { get; set; }

        public string? Address { get; set; }

        public string? DepartmentName { get; set; }
    }
}
