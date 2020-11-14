using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EDiary.Data.Migrations
{
    public partial class AddStudentsParents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentsParents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    StudentId = table.Column<string>(nullable: true),
                    ParentId = table.Column<string>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentsParents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentsParents_AspNetUsers_ParentId",
                        column: x => x.ParentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentsParents_AspNetUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentsParents_IsDeleted",
                table: "StudentsParents",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_StudentsParents_ParentId",
                table: "StudentsParents",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentsParents_StudentId",
                table: "StudentsParents",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentsParents");
        }
    }
}
