using Microsoft.EntityFrameworkCore.Migrations;

namespace EDiary.Data.Migrations
{
    public partial class ChangeApplicationUserUCNtoString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UniqueCitizenshipNumber",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UniqueCitizenshipNumber",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UniqueCitizenshipNumber",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UniqueCitizenshipNumber",
                table: "AspNetUsers",
                column: "UniqueCitizenshipNumber",
                unique: true);
        }
    }
}
