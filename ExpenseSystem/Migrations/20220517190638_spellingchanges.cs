using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseSystem.Migrations
{
    public partial class spellingchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpensesLines_Expenses_ExpenseId",
                table: "ExpensesLines");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpensesLines_Items_ItemId",
                table: "ExpensesLines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExpensesLines",
                table: "ExpensesLines");

            migrationBuilder.RenameTable(
                name: "ExpensesLines",
                newName: "ExpenseLines");

            migrationBuilder.RenameIndex(
                name: "IX_ExpensesLines_ItemId",
                table: "ExpenseLines",
                newName: "IX_ExpenseLines_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_ExpensesLines_ExpenseId",
                table: "ExpenseLines",
                newName: "IX_ExpenseLines_ExpenseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExpenseLines",
                table: "ExpenseLines",
                column: "Id");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseLines_Expenses_ExpenseId",
                table: "ExpenseLines");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseLines_Items_ItemId",
                table: "ExpenseLines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExpenseLines",
                table: "ExpenseLines");

            migrationBuilder.RenameTable(
                name: "ExpenseLines",
                newName: "ExpensesLines");

            migrationBuilder.RenameIndex(
                name: "IX_ExpenseLines_ItemId",
                table: "ExpensesLines",
                newName: "IX_ExpensesLines_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_ExpenseLines_ExpenseId",
                table: "ExpensesLines",
                newName: "IX_ExpensesLines_ExpenseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExpensesLines",
                table: "ExpensesLines",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpensesLines_Expenses_ExpenseId",
                table: "ExpensesLines",
                column: "ExpenseId",
                principalTable: "Expenses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpensesLines_Items_ItemId",
                table: "ExpensesLines",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id");
        }
    }
}
