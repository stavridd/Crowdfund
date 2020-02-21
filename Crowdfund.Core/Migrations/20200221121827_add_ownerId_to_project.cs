using Microsoft.EntityFrameworkCore.Migrations;

namespace Crowdfund.Core.Migrations
{
    public partial class add_ownerId_to_project : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_Owner_OwnerId",
                table: "Project");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Project",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Owner_OwnerId",
                table: "Project",
                column: "OwnerId",
                principalTable: "Owner",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_Owner_OwnerId",
                table: "Project");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Project",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Owner_OwnerId",
                table: "Project",
                column: "OwnerId",
                principalTable: "Owner",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
