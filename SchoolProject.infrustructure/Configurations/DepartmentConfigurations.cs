using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data._ُEntities;

namespace SchoolProject.infrustructure.Configurations
{
    public class DepartmentConfigurations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {

            builder.HasKey(x => x.DIO);

            builder.HasMany(x => x.Students)
                .WithOne(x => x.Departments)
                .HasForeignKey(x => x.DIO)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Instructor).
                 WithOne(x => x.departmentManager).
                 HasForeignKey<Department>(x => x.InsManager).
                 OnDelete(DeleteBehavior.Restrict);

        }
    }
}
