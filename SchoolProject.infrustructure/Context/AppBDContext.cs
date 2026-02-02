using Microsoft.EntityFrameworkCore;
using SchoolProject.Data._ُEntities;
using SchoolProject.Data.Entities;
using System.Reflection;

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
        public DbSet<Department> departments { get; set; }
        public DbSet<Student> students { get; set; }
        public DbSet<DepartmentSubject> departmentSubjects { get; set; }
        public DbSet<Subjects> subjects { get; set; }
        public DbSet<StudentSubject> studentSubjects { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
