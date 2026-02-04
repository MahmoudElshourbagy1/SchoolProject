using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolProject.infrustructure.Migrations
{
    /// <inheritdoc />
    public partial class fixDepartmentStudentCountProc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DID",
                table: "ViewDepartment",
                newName: "DIO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DIO",
                table: "ViewDepartment",
                newName: "DID");
        }
    }
}
