namespace EDiary.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ScheduleSubjectsClasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScheduleSubjectsClasses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    StartAt = table.Column<DateTime>(nullable: false),
                    FinishAt = table.Column<DateTime>(nullable: false),
                    DayOfWeek = table.Column<int>(nullable: false),
                    SubjectClassId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleSubjectsClasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleSubjectsClasses_SubjectsClasses_SubjectClassId",
                        column: x => x.SubjectClassId,
                        principalTable: "SubjectsClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleSubjectsClasses_IsDeleted",
                table: "ScheduleSubjectsClasses",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleSubjectsClasses_SubjectClassId",
                table: "ScheduleSubjectsClasses",
                column: "SubjectClassId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduleSubjectsClasses");
        }
    }
}
