using Microsoft.EntityFrameworkCore.Migrations;

namespace OnChurch.Web.Migrations
{
    public partial class DeleteCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Churches_Sections_SectionId",
                table: "Churches");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Campuses_CampusId1",
                table: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_Sections_CampusId1",
                table: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_Sections_Name",
                table: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_Professions_Name",
                table: "Professions");

            migrationBuilder.DropColumn(
                name: "CampusId1",
                table: "Sections");

            migrationBuilder.AddColumn<int>(
                name: "CampusId",
                table: "Sections",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdSection",
                table: "Churches",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sections_CampusId",
                table: "Sections",
                column: "CampusId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_Name_CampusId",
                table: "Sections",
                columns: new[] { "Name", "CampusId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Churches_Name_IdSection",
                table: "Churches",
                columns: new[] { "Name", "IdSection" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Churches_Sections_SectionId",
                table: "Churches",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Campuses_CampusId",
                table: "Sections",
                column: "CampusId",
                principalTable: "Campuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Churches_Sections_SectionId",
                table: "Churches");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Campuses_CampusId",
                table: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_Sections_CampusId",
                table: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_Sections_Name_CampusId",
                table: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_Churches_Name_IdSection",
                table: "Churches");

            migrationBuilder.DropColumn(
                name: "CampusId",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "IdSection",
                table: "Churches");

            migrationBuilder.AddColumn<int>(
                name: "CampusId1",
                table: "Sections",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sections_CampusId1",
                table: "Sections",
                column: "CampusId1");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_Name",
                table: "Sections",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Professions_Name",
                table: "Professions",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Churches_Sections_SectionId",
                table: "Churches",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Campuses_CampusId1",
                table: "Sections",
                column: "CampusId1",
                principalTable: "Campuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
