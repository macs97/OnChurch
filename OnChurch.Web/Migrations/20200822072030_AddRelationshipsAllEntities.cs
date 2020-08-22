using Microsoft.EntityFrameworkCore.Migrations;

namespace OnChurch.Web.Migrations
{
    public partial class AddRelationshipsAllEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Church_Profession_ProfessionId",
                table: "Church");

            migrationBuilder.DropForeignKey(
                name: "FK_Church_Section_SectionId",
                table: "Church");

            migrationBuilder.DropForeignKey(
                name: "FK_Member_Church_ChurchId",
                table: "Member");

            migrationBuilder.DropForeignKey(
                name: "FK_Section_Campuses_CampusId",
                table: "Section");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Section",
                table: "Section");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Profession",
                table: "Profession");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Member",
                table: "Member");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Church",
                table: "Church");

            migrationBuilder.RenameTable(
                name: "Section",
                newName: "Sections");

            migrationBuilder.RenameTable(
                name: "Profession",
                newName: "Professions");

            migrationBuilder.RenameTable(
                name: "Member",
                newName: "Members");

            migrationBuilder.RenameTable(
                name: "Church",
                newName: "Churches");

            migrationBuilder.RenameIndex(
                name: "IX_Section_CampusId",
                table: "Sections",
                newName: "IX_Sections_CampusId");

            migrationBuilder.RenameIndex(
                name: "IX_Member_ChurchId",
                table: "Members",
                newName: "IX_Members_ChurchId");

            migrationBuilder.RenameIndex(
                name: "IX_Church_SectionId",
                table: "Churches",
                newName: "IX_Churches_SectionId");

            migrationBuilder.RenameIndex(
                name: "IX_Church_ProfessionId",
                table: "Churches",
                newName: "IX_Churches_ProfessionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sections",
                table: "Sections",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Professions",
                table: "Professions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Members",
                table: "Members",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Churches",
                table: "Churches",
                column: "Id");

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
                name: "FK_Churches_Professions_ProfessionId",
                table: "Churches",
                column: "ProfessionId",
                principalTable: "Professions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Churches_Sections_SectionId",
                table: "Churches",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Churches_ChurchId",
                table: "Members",
                column: "ChurchId",
                principalTable: "Churches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Campuses_CampusId",
                table: "Sections",
                column: "CampusId",
                principalTable: "Campuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Churches_Professions_ProfessionId",
                table: "Churches");

            migrationBuilder.DropForeignKey(
                name: "FK_Churches_Sections_SectionId",
                table: "Churches");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_Churches_ChurchId",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Campuses_CampusId",
                table: "Sections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sections",
                table: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_Sections_Name",
                table: "Sections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Professions",
                table: "Professions");

            migrationBuilder.DropIndex(
                name: "IX_Professions_Name",
                table: "Professions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Members",
                table: "Members");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Churches",
                table: "Churches");

            migrationBuilder.RenameTable(
                name: "Sections",
                newName: "Section");

            migrationBuilder.RenameTable(
                name: "Professions",
                newName: "Profession");

            migrationBuilder.RenameTable(
                name: "Members",
                newName: "Member");

            migrationBuilder.RenameTable(
                name: "Churches",
                newName: "Church");

            migrationBuilder.RenameIndex(
                name: "IX_Sections_CampusId",
                table: "Section",
                newName: "IX_Section_CampusId");

            migrationBuilder.RenameIndex(
                name: "IX_Members_ChurchId",
                table: "Member",
                newName: "IX_Member_ChurchId");

            migrationBuilder.RenameIndex(
                name: "IX_Churches_SectionId",
                table: "Church",
                newName: "IX_Church_SectionId");

            migrationBuilder.RenameIndex(
                name: "IX_Churches_ProfessionId",
                table: "Church",
                newName: "IX_Church_ProfessionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Section",
                table: "Section",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Profession",
                table: "Profession",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Member",
                table: "Member",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Church",
                table: "Church",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Church_Profession_ProfessionId",
                table: "Church",
                column: "ProfessionId",
                principalTable: "Profession",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Church_Section_SectionId",
                table: "Church",
                column: "SectionId",
                principalTable: "Section",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Member_Church_ChurchId",
                table: "Member",
                column: "ChurchId",
                principalTable: "Church",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Section_Campuses_CampusId",
                table: "Section",
                column: "CampusId",
                principalTable: "Campuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
