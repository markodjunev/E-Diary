namespace EDiary.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class SubjectClassSchoolId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SchoolId",
                table: "SubjectsClasses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SubjectsClasses_SchoolId",
                table: "SubjectsClasses",
                column: "SchoolId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectsClasses_Schools_SchoolId",
                table: "SubjectsClasses",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectsClasses_Schools_SchoolId",
                table: "SubjectsClasses");

            migrationBuilder.DropIndex(
                name: "IX_SubjectsClasses_SchoolId",
                table: "SubjectsClasses");

            migrationBuilder.DropColumn(
                name: "SchoolId",
                table: "SubjectsClasses");
        }
    }
}
