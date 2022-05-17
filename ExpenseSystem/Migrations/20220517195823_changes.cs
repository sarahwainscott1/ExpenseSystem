using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseSystem.Migrations
{
    public partial class changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseLines_Expenses_ExpenseId",
                table: "ExpenseLines");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseLines_Items_ItemId",
                table: "ExpenseLines");

            migrationBuilder.AlterColumn<int>(
                name: "ItemId",
                table: "ExpenseLines",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ExpenseId",
                table: "ExpenseLines",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseLines_Expenses_ExpenseId",
                table: "ExpenseLines",
                column: "ExpenseId",
                principalTable: "Expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseLines_Items_ItemId",
                table: "ExpenseLines",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseLines_Expenses_ExpenseId",
                table: "ExpenseLines");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseLines_Items_ItemId",
                table: "ExpenseLines");

            migrationBuilder.AlterColumn<int>(
                name: "ItemId",
                table: "ExpenseLines",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ExpenseId",
                table: "ExpenseLines",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseLines_Expenses_ExpenseId",
                table: "ExpenseLines",
                column: "ExpenseId",
                principalTable: "Expenses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseLines_Items_ItemId",
                table: "ExpenseLines",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id");
        }
    }
}
