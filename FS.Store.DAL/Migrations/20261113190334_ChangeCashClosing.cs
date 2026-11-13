using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FS.Store.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCashClosing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_cash_closing_cash_register_session_id",
                table: "cash_closing");

            migrationBuilder.RenameColumn(
                name: "cash_amount",
                table: "cash_closing",
                newName: "cash_closing_amount");

            migrationBuilder.AlterColumn<decimal>(
                name: "total_pix",
                table: "cash_closing",
                type: "decimal(10,2)",
                nullable: false,
                comment: "Valor total de PIX recebido em todas as maquininhas",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "Valor total de PIX");

            migrationBuilder.AlterColumn<decimal>(
                name: "total_debit",
                table: "cash_closing",
                type: "decimal(10,2)",
                nullable: false,
                comment: "Valor total de débito passado em todas as maquininhas",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "Valor total de débito");

            migrationBuilder.AlterColumn<decimal>(
                name: "total_credit",
                table: "cash_closing",
                type: "decimal(10,2)",
                nullable: false,
                comment: "Valor total de crédito passado em todas as maquininhas",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "Valor total de crédito");

            migrationBuilder.AlterColumn<decimal>(
                name: "cash_closing_amount",
                table: "cash_closing",
                type: "decimal(10,2)",
                nullable: false,
                comment: "Valor total em dinheiro encontrado no fechamento do caixa",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "Valor total em dinheiro");

            migrationBuilder.AddColumn<decimal>(
                name: "total_cash_inflows",
                table: "cash_closing",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "Valor total de aportes em dinheiro");

            migrationBuilder.AddColumn<decimal>(
                name: "total_expenses",
                table: "cash_closing",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "Valor total de despesas pago em dinheiro");

            migrationBuilder.AddColumn<decimal>(
                name: "total_withdrawal",
                table: "cash_closing",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "Valor total de saques realizado em dinheiro");

            migrationBuilder.CreateIndex(
                name: "IX_cash_closing_cash_register_session_id",
                table: "cash_closing",
                column: "cash_register_session_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_cash_closing_cash_register_session_id",
                table: "cash_closing");

            migrationBuilder.DropColumn(
                name: "total_cash_inflows",
                table: "cash_closing");

            migrationBuilder.DropColumn(
                name: "total_expenses",
                table: "cash_closing");

            migrationBuilder.DropColumn(
                name: "total_withdrawal",
                table: "cash_closing");

            migrationBuilder.RenameColumn(
                name: "cash_closing_amount",
                table: "cash_closing",
                newName: "cash_amount");

            migrationBuilder.AlterColumn<decimal>(
                name: "total_pix",
                table: "cash_closing",
                type: "decimal(18,2)",
                nullable: false,
                comment: "Valor total de PIX",
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldComment: "Valor total de PIX recebido em todas as maquininhas");

            migrationBuilder.AlterColumn<decimal>(
                name: "total_debit",
                table: "cash_closing",
                type: "decimal(18,2)",
                nullable: false,
                comment: "Valor total de débito",
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldComment: "Valor total de débito passado em todas as maquininhas");

            migrationBuilder.AlterColumn<decimal>(
                name: "total_credit",
                table: "cash_closing",
                type: "decimal(18,2)",
                nullable: false,
                comment: "Valor total de crédito",
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldComment: "Valor total de crédito passado em todas as maquininhas");

            migrationBuilder.AlterColumn<decimal>(
                name: "cash_amount",
                table: "cash_closing",
                type: "decimal(18,2)",
                nullable: false,
                comment: "Valor total em dinheiro",
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldComment: "Valor total em dinheiro encontrado no fechamento do caixa");

            migrationBuilder.CreateIndex(
                name: "IX_cash_closing_cash_register_session_id",
                table: "cash_closing",
                column: "cash_register_session_id");
        }
    }
}
