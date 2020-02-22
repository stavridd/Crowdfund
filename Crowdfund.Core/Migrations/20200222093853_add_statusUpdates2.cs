using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Crowdfund.Core.Migrations
{
    public partial class add_statusUpdates2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StatusUpdates_Project_ProjectId",
                table: "StatusUpdates");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "StatusUpdates",
                newName: "projectId");

            migrationBuilder.RenameIndex(
                name: "IX_StatusUpdates_ProjectId",
                table: "StatusUpdates",
                newName: "IX_StatusUpdates_projectId");

            migrationBuilder.AlterColumn<int>(
                name: "projectId",
                table: "StatusUpdates",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DatePost",
                table: "StatusUpdates",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddForeignKey(
                name: "FK_StatusUpdates_Project_projectId",
                table: "StatusUpdates",
                column: "projectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StatusUpdates_Project_projectId",
                table: "StatusUpdates");

            migrationBuilder.DropColumn(
                name: "DatePost",
                table: "StatusUpdates");

            migrationBuilder.RenameColumn(
                name: "projectId",
                table: "StatusUpdates",
                newName: "ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_StatusUpdates_projectId",
                table: "StatusUpdates",
                newName: "IX_StatusUpdates_ProjectId");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "StatusUpdates",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_StatusUpdates_Project_ProjectId",
                table: "StatusUpdates",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
