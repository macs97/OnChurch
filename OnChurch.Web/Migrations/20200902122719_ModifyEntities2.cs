using Microsoft.EntityFrameworkCore.Migrations;

namespace OnChurch.Web.Migrations
{
    public partial class ModifyEntities2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Churches_ChurchId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "ChurchId",
                table: "AspNetUsers",
                newName: "ChurchId1");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_ChurchId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_ChurchId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Churches_ChurchId1",
                table: "AspNetUsers",
                column: "ChurchId1",
                principalTable: "Churches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Churches_ChurchId1",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "ChurchId1",
                table: "AspNetUsers",
                newName: "ChurchId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_ChurchId1",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_ChurchId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Churches_ChurchId",
                table: "AspNetUsers",
                column: "ChurchId",
                principalTable: "Churches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
