using Microsoft.EntityFrameworkCore.Migrations;

namespace Crowdfund.Core.Migrations
{
    public partial class m2m_7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buyer_Progress_ProgressOwnerId_ProgressProjectId",
                table: "Buyer");

            migrationBuilder.DropForeignKey(
                name: "FK_Progress_Owner_OwnerId",
                table: "Progress");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Progress",
                table: "Progress");

            migrationBuilder.DropIndex(
                name: "IX_Buyer_ProgressOwnerId_ProgressProjectId",
                table: "Buyer");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Progress");

            migrationBuilder.DropColumn(
                name: "ProgressOwnerId",
                table: "Buyer");

            migrationBuilder.DropColumn(
                name: "ProgressProjectId",
                table: "Buyer");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Progress",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ProgressId",
                table: "Progress",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ProgressId",
                table: "Buyer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Progress",
                table: "Progress",
                column: "ProgressId");

            migrationBuilder.CreateIndex(
                name: "IX_Progress_OwnerId",
                table: "Progress",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Buyer_ProgressId",
                table: "Buyer",
                column: "ProgressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buyer_Progress_ProgressId",
                table: "Buyer",
                column: "ProgressId",
                principalTable: "Progress",
                principalColumn: "ProgressId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Progress_Owner_OwnerId",
                table: "Progress",
                column: "OwnerId",
                principalTable: "Owner",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buyer_Progress_ProgressId",
                table: "Buyer");

            migrationBuilder.DropForeignKey(
                name: "FK_Progress_Owner_OwnerId",
                table: "Progress");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Progress",
                table: "Progress");

            migrationBuilder.DropIndex(
                name: "IX_Progress_OwnerId",
                table: "Progress");

            migrationBuilder.DropIndex(
                name: "IX_Buyer_ProgressId",
                table: "Buyer");

            migrationBuilder.DropColumn(
                name: "ProgressId",
                table: "Progress");

            migrationBuilder.DropColumn(
                name: "ProgressId",
                table: "Buyer");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Progress",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Progress",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProgressOwnerId",
                table: "Buyer",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProgressProjectId",
                table: "Buyer",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Progress",
                table: "Progress",
                columns: new[] { "OwnerId", "ProjectId" });

            migrationBuilder.CreateIndex(
                name: "IX_Buyer_ProgressOwnerId_ProgressProjectId",
                table: "Buyer",
                columns: new[] { "ProgressOwnerId", "ProgressProjectId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Buyer_Progress_ProgressOwnerId_ProgressProjectId",
                table: "Buyer",
                columns: new[] { "ProgressOwnerId", "ProgressProjectId" },
                principalTable: "Progress",
                principalColumns: new[] { "OwnerId", "ProjectId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Progress_Owner_OwnerId",
                table: "Progress",
                column: "OwnerId",
                principalTable: "Owner",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
