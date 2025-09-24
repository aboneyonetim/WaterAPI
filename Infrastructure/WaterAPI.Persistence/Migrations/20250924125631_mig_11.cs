using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WaterAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CardRegisters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AppUserId = table.Column<string>(type: "text", nullable: false),
                    Number = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardRegisters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardRegisters_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardPayloads",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CardRegisterId = table.Column<Guid>(type: "uuid", nullable: false),
                    PreviousBalance = table.Column<float>(type: "real", nullable: false),
                    LoadedBalance = table.Column<float>(type: "real", nullable: false),
                    TotalBalance = table.Column<float>(type: "real", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardPayloads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardPayloads_CardRegisters_CardRegisterId",
                        column: x => x.CardRegisterId,
                        principalTable: "CardRegisters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardPayloads_CardRegisterId",
                table: "CardPayloads",
                column: "CardRegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_CardRegisters_AppUserId",
                table: "CardRegisters",
                column: "AppUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardPayloads");

            migrationBuilder.DropTable(
                name: "CardRegisters");
        }
    }
}
