using MediatR;
using SchoolProject.Core.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SchoolProject.Core.Features.Students.Commands.Models
{
    public class AddStudentCommand :IRequest<Response<string>>
    {
        [Required]
        public string Name { get; set; }
        [Required]

        public string Address { get; set; }

        public int Phone { get; set; }
        public int DepartmementId { get; set; }
    }
}
