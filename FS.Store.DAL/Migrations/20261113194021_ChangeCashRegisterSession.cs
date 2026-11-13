using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FS.Store.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCashRegisterSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "total_sales",
                table: "cash_register_session",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "Valor total vendido");

            migrationBuilder.Sql(@"
                UPDATE cash_register_session SET total_sales = (
    SELECT total_withdrawal + total_expenses + total_cash_inflows + total_pix + total_debit + total_credit + cash_closing_amount 
    from cash_closing WHERE cash_closing.cash_register_session_id = cash_register_session.id
) - opening_amount WHERE 1=1;"
);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "total_sales",
                table: "cash_register_session");
        }
    }
}
