using Microsoft.EntityFrameworkCore.Migrations;

namespace Crowdfund.Core.Migrations
{
    public partial class m2m_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProgressOwnerId",
                table: "Buyer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProgressProjectId",
                table: "Buyer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Buyer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Progress",
                columns: table => new
                {
                    OwnerId = table.Column<int>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false),
                    Goal = table.Column<decimal>(nullable: false),
                    Contributions = table.Column<decimal>(nullable: false),
                    NumberOfContributions = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Progress", x => new { x.OwnerId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_Progress_Owner_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Buyer_ProjectId",
                table: "Buyer",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Buyer_ProgressOwnerId_ProgressProjectId",
                table: "Buyer",
                columns: new[] { "ProgressOwnerId", "ProgressProjectId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Buyer_Project_ProjectId",
                table: "Buyer",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Buyer_Progress_ProgressOwnerId_ProgressProjectId",
                table: "Buyer",
                columns: new[] { "ProgressOwnerId", "ProgressProjectId" },
                principalTable: "Progress",
                principalColumns: new[] { "OwnerId", "ProjectId" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buyer_Project_ProjectId",
                table: "Buyer");

            migrationBuilder.DropForeignKey(
                name: "FK_Buyer_Progress_ProgressOwnerId_ProgressProjectId",
                table: "Buyer");

            migrationBuilder.DropTable(
                name: "Progress");

            migrationBuilder.DropIndex(
                name: "IX_Buyer_ProjectId",
                table: "Buyer");

            migrationBuilder.DropIndex(
                name: "IX_Buyer_ProgressOwnerId_ProgressProjectId",
                table: "Buyer");

            migrationBuilder.DropColumn(
                name: "ProgressOwnerId",
                table: "Buyer");

            migrationBuilder.DropColumn(
                name: "ProgressProjectId",
                table: "Buyer");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Buyer");
        }
    }
}
