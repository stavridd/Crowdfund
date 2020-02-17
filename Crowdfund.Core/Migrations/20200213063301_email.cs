using Microsoft.EntityFrameworkCore.Migrations;

namespace Crowdfund.Core.Migrations
{
    public partial class email : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Lastname",
                table: "Owner",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Firstname",
                table: "Owner",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "Lastname",
                table: "Buyer",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Firstname",
                table: "Buyer",
                newName: "FirstName");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Owner",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Buyer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Owner_Email",
                table: "Owner",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Buyer_Email",
                table: "Buyer",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Owner_Email",
                table: "Owner");

            migrationBuilder.DropIndex(
                name: "IX_Buyer_Email",
                table: "Buyer");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Owner");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Buyer");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Owner",
                newName: "Lastname");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Owner",
                newName: "Firstname");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Buyer",
                newName: "Lastname");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Buyer",
                newName: "Firstname");
        }
    }
}
