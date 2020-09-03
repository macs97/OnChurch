using Microsoft.EntityFrameworkCore.Migrations;

namespace OnChurch.Web.Migrations
{
    public partial class RemoveColumnPhone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "AspNetUsers",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }
    }
}
