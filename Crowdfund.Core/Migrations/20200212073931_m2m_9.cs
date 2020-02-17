using Microsoft.EntityFrameworkCore.Migrations;

namespace Crowdfund.Core.Migrations
{
    public partial class m2m_9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buyer_Progress_ProgressId",
                table: "Buyer");

            migrationBuilder.DropForeignKey(
                name: "FK_Buyer_Project_ProjectId",
                table: "Buyer");

            migrationBuilder.DropForeignKey(
                name: "FK_Reward_Buyer_BuyerId",
                table: "Reward");

            migrationBuilder.DropTable(
                name: "Progress");

            migrationBuilder.DropIndex(
                name: "IX_Reward_BuyerId",
                table: "Reward");

            migrationBuilder.DropIndex(
                name: "IX_Buyer_ProgressId",
                table: "Buyer");

            migrationBuilder.DropIndex(
                name: "IX_Buyer_ProjectId",
                table: "Buyer");

            migrationBuilder.DropColumn(
                name: "BuyerId",
                table: "Reward");

            migrationBuilder.DropColumn(
                name: "ProgressId",
                table: "Buyer");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Buyer");

            migrationBuilder.CreateTable(
                name: "BuyerReward",
                columns: table => new
                {
                    BuyerId = table.Column<int>(nullable: false),
                    RewardId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyerReward", x => new { x.BuyerId, x.RewardId });
                    table.ForeignKey(
                        name: "FK_BuyerReward_Buyer_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "Buyer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuyerReward_Reward_RewardId",
                        column: x => x.RewardId,
                        principalTable: "Reward",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BuyerReward_RewardId",
                table: "BuyerReward",
                column: "RewardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuyerReward");

            migrationBuilder.AddColumn<int>(
                name: "BuyerId",
                table: "Reward",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProgressId",
                table: "Buyer",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Buyer",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Progress",
                columns: table => new
                {
                    ProgressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Contributions = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Goal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NumberOfContributions = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Progress", x => x.ProgressId);
                    table.ForeignKey(
                        name: "FK_Progress_Owner_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reward_BuyerId",
                table: "Reward",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Buyer_ProgressId",
                table: "Buyer",
                column: "ProgressId");

            migrationBuilder.CreateIndex(
                name: "IX_Buyer_ProjectId",
                table: "Buyer",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Progress_OwnerId",
                table: "Progress",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buyer_Progress_ProgressId",
                table: "Buyer",
                column: "ProgressId",
                principalTable: "Progress",
                principalColumn: "ProgressId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Buyer_Project_ProjectId",
                table: "Buyer",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reward_Buyer_BuyerId",
                table: "Reward",
                column: "BuyerId",
                principalTable: "Buyer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
