using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;


namespace SchoolProject.infrustructure.Configurations
{
    public class DepartmentSubjectConfigurations : IEntityTypeConfiguration<DepartmentSubject>
    {
        public void Configure(EntityTypeBuilder<DepartmentSubject> builder)
        {
            builder.HasKey(x => new { x.SubID, x.DIO });

            builder.HasOne(ds => ds.Department)
                 .WithMany(d => d.DepartmentsSubjects)
                 .HasForeignKey(ds => ds.DIO);

            builder.HasOne(ds => ds.Subjects)
                 .WithMany(d => d.DepartmentsSubjects)
                 .HasForeignKey(ds => ds.SubID);
        }
    }
}
