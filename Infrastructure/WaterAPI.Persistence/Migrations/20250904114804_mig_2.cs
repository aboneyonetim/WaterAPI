using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WaterAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Costomers_CustomerId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Costomers",
                table: "Costomers");

            migrationBuilder.RenameTable(
                name: "Costomers",
                newName: "Customers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Costomers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Costomers",
                table: "Costomers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Costomers_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Costomers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
