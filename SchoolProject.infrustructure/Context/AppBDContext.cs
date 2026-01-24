using Microsoft.EntityFrameworkCore;
using SchoolProject.Data._ُEntities;
using SchoolProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.infrustructure.Data
{
    public class AppBDContext : DbContext
    {
        public AppBDContext(DbContextOptions<AppBDContext> options) : base(options)
        {
        }

        public AppBDContext()
        {
        }
        public DbSet<Department>departments { get; set; }
        public DbSet<Student> students { get; set; }
        public DbSet<DepartmetSubject>departmetSubjects { get; set; }
        public DbSet<Subjects>subjects { get; set; }
        public DbSet<StudentSubject> studentSubjects { get; set; }

    }
}
