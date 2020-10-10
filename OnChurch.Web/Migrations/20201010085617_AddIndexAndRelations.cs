using Microsoft.EntityFrameworkCore.Migrations;

namespace OnChurch.Web.Migrations
{
    public partial class AddIndexAndRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Meetings_Id",
                table: "Meetings",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Assistances_Id",
                table: "Assistances",
                column: "Id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Meetings_Id",
                table: "Meetings");

            migrationBuilder.DropIndex(
                name: "IX_Assistances_Id",
                table: "Assistances");
        }
    }
}
