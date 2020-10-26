namespace EDiary.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddUniqueCitizenshipNumberToApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UniqueCitizenshipNumber",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UniqueCitizenshipNumber",
                table: "AspNetUsers",
                column: "UniqueCitizenshipNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UniqueCitizenshipNumber",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UniqueCitizenshipNumber",
                table: "AspNetUsers");
        }
    }
}
