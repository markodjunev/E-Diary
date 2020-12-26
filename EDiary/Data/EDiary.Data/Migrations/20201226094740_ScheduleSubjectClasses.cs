namespace EDiary.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ScheduleSubjectClasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScheduleSubjectClasses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    DayOfWeek = table.Column<int>(nullable: false),
                    StartAt = table.Column<DateTime>(nullable: false),
                    FinishAt = table.Column<DateTime>(nullable: false),
                    SubjecClassId = table.Column<int>(nullable: false),
                    SubjectClassId = table.Column<int>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleSubjectClasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleSubjectClasses_SubjectsClasses_SubjectClassId",
                        column: x => x.SubjectClassId,
                        principalTable: "SubjectsClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleSubjectClasses_IsDeleted",
                table: "ScheduleSubjectClasses",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleSubjectClasses_SubjectClassId",
                table: "ScheduleSubjectClasses",
                column: "SubjectClassId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduleSubjectClasses");
        }
    }
}
