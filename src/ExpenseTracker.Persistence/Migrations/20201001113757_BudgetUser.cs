using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpenseTracker.Persistence.Migrations
{
    public partial class BudgetUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BudgetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    InsertUserId = table.Column<string>(nullable: true),
                    InsertTime = table.Column<DateTime>(nullable: false),
                    UpdateUserId = table.Column<string>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    BudgetId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Role = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BudgetUsers_Budgets_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "Budgets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BudgetUsers_AspNetUsers_InsertUserId",
                        column: x => x.InsertUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BudgetUsers_AspNetUsers_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BudgetUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BudgetUsers_BudgetId",
                table: "BudgetUsers",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetUsers_InsertUserId",
                table: "BudgetUsers",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetUsers_UpdateUserId",
                table: "BudgetUsers",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetUsers_UserId",
                table: "BudgetUsers",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BudgetUsers");
        }
    }
}
