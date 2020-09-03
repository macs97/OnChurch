using Microsoft.EntityFrameworkCore.Migrations;

namespace OnChurch.Web.Migrations
{
    public partial class ModifyEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Campuses_CampusId",
                table: "Sections");

            migrationBuilder.RenameColumn(
                name: "CampusId",
                table: "Sections",
                newName: "CampusId1");

            migrationBuilder.RenameIndex(
                name: "IX_Sections_CampusId",
                table: "Sections",
                newName: "IX_Sections_CampusId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Campuses_CampusId1",
                table: "Sections",
                column: "CampusId1",
                principalTable: "Campuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Campuses_CampusId1",
                table: "Sections");

            migrationBuilder.RenameColumn(
                name: "CampusId1",
                table: "Sections",
                newName: "CampusId");

            migrationBuilder.RenameIndex(
                name: "IX_Sections_CampusId1",
                table: "Sections",
                newName: "IX_Sections_CampusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Campuses_CampusId",
                table: "Sections",
                column: "CampusId",
                principalTable: "Campuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
