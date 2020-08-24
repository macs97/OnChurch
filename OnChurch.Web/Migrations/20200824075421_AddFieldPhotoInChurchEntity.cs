using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnChurch.Web.Migrations
{
    public partial class AddFieldPhotoInChurchEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PhotoId",
                table: "Members",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "Members");
        }
    }
}
