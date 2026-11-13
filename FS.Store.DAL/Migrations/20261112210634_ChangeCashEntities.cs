using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FS.Store.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCashEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cash_closing_terminal_cash_closing_CashClosingId",
                table: "cash_closing_terminal");

            migrationBuilder.RenameColumn(
                name: "CashClosingId",
                table: "cash_closing_terminal",
                newName: "cash_closing_id");

            migrationBuilder.RenameIndex(
                name: "IX_cash_closing_terminal_CashClosingId",
                table: "cash_closing_terminal",
                newName: "IX_cash_closing_terminal_cash_closing_id");

            migrationBuilder.AlterColumn<long>(
                name: "cash_closing_id",
                table: "cash_closing_terminal",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "closed_at",
                table: "cash_closing_terminal",
                type: "datetime(0)",
                nullable: true,
                defaultValueSql: "CURRENT_TIMESTAMP",
                comment: "Data/Hora de fechamento");

            migrationBuilder.AddForeignKey(
                name: "FK_cash_closing_terminal_cash_closing_cash_closing_id",
                table: "cash_closing_terminal",
                column: "cash_closing_id",
                principalTable: "cash_closing",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.Sql(@"
UPDATE cash_closing_terminal set cash_closing_id = (SELECT id FROM cash_closing WHERE cash_register_session_id = cash_closing_terminal.cash_register_session_id LIMIT 1) 
WHERE cash_closing_id IS NULL;
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cash_closing_terminal_cash_closing_cash_closing_id",
                table: "cash_closing_terminal");

            migrationBuilder.DropColumn(
                name: "closed_at",
                table: "cash_closing_terminal");

            migrationBuilder.RenameColumn(
                name: "cash_closing_id",
                table: "cash_closing_terminal",
                newName: "CashClosingId");

            migrationBuilder.RenameIndex(
                name: "IX_cash_closing_terminal_cash_closing_id",
                table: "cash_closing_terminal",
                newName: "IX_cash_closing_terminal_CashClosingId");

            migrationBuilder.AlterColumn<long>(
                name: "CashClosingId",
                table: "cash_closing_terminal",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_cash_closing_terminal_cash_closing_CashClosingId",
                table: "cash_closing_terminal",
                column: "CashClosingId",
                principalTable: "cash_closing",
                principalColumn: "id");
        }
    }
}
