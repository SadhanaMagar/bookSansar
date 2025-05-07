using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace bookSansar.Migrations
{
    /// <inheritdoc />
    public partial class AddPurchaseAndReviewConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reviews_BookId_UserId",
                table: "Reviews");

            migrationBuilder.AddColumn<int>(
                name: "PurchaseId",
                table: "Reviews",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    BookId = table.Column<int>(type: "integer", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    TransactionId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_PurchaseId",
                table: "Reviews",
                column: "PurchaseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_BookId_UserId",
                table: "Purchases",
                columns: new[] { "BookId", "UserId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Purchases_PurchaseId",
                table: "Reviews",
                column: "PurchaseId",
                principalTable: "Purchases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Purchases_PurchaseId",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_PurchaseId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "PurchaseId",
                table: "Reviews");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_BookId_UserId",
                table: "Reviews",
                columns: new[] { "BookId", "UserId" },
                unique: true);
        }
    }
}
