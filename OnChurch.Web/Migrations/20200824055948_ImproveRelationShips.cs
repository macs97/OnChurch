using Microsoft.EntityFrameworkCore.Migrations;

namespace OnChurch.Web.Migrations
{
    public partial class ImproveRelationShips : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Churches_Professions_ProfessionId",
                table: "Churches");

            migrationBuilder.DropIndex(
                name: "IX_Churches_ProfessionId",
                table: "Churches");

            migrationBuilder.DropColumn(
                name: "ProfessionId",
                table: "Churches");

            migrationBuilder.AddColumn<int>(
                name: "ProfessionId",
                table: "Members",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_ProfessionId",
                table: "Members",
                column: "ProfessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Professions_ProfessionId",
                table: "Members",
                column: "ProfessionId",
                principalTable: "Professions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Professions_ProfessionId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_ProfessionId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "ProfessionId",
                table: "Members");

            migrationBuilder.AddColumn<int>(
                name: "ProfessionId",
                table: "Churches",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Churches_ProfessionId",
                table: "Churches",
                column: "ProfessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Churches_Professions_ProfessionId",
                table: "Churches",
                column: "ProfessionId",
                principalTable: "Professions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
