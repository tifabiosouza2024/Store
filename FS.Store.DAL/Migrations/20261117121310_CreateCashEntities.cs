using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FS.Store.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CreateCashEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "payment_terminal",
                type: "datetime(0)",
                nullable: true,
                defaultValueSql: "CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP",
                comment: "Data de atualização do registro",
                oldClrType: typeof(DateTime),
                oldType: "datetime(0)",
                oldNullable: true,
                oldComment: "Data de atualização do registro");

            migrationBuilder.AlterColumn<bool>(
                name: "deleted",
                table: "payment_terminal",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                comment: "registro deletado virtualmente",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "registro deletado virtualmente");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "payment_terminal",
                type: "datetime(0)",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                comment: "Data de criação do registro",
                oldClrType: typeof(DateTime),
                oldType: "datetime(0)",
                oldComment: "Data de criação do registro");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "payment_provider",
                type: "datetime(0)",
                nullable: true,
                defaultValueSql: "CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP",
                comment: "Data de atualização do registro",
                oldClrType: typeof(DateTime),
                oldType: "datetime(0)",
                oldNullable: true,
                oldComment: "Data de atualização do registro");

            migrationBuilder.AlterColumn<bool>(
                name: "deleted",
                table: "payment_provider",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                comment: "registro deletado virtualmente",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "registro deletado virtualmente");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "payment_provider",
                type: "datetime(0)",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                comment: "Data de criação do registro",
                oldClrType: typeof(DateTime),
                oldType: "datetime(0)",
                oldComment: "Data de criação do registro");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "cash_register_session",
                type: "datetime(0)",
                nullable: true,
                defaultValueSql: "CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP",
                comment: "Data de atualização do registro",
                oldClrType: typeof(DateTime),
                oldType: "datetime(0)",
                oldNullable: true,
                oldComment: "Data de atualização do registro");

            migrationBuilder.AlterColumn<bool>(
                name: "deleted",
                table: "cash_register_session",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                comment: "registro deletado virtualmente",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "registro deletado virtualmente");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "cash_register_session",
                type: "datetime(0)",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                comment: "Data de criação do registro",
                oldClrType: typeof(DateTime),
                oldType: "datetime(0)",
                oldComment: "Data de criação do registro");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "cash_movement",
                type: "datetime(0)",
                nullable: true,
                defaultValueSql: "CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP",
                comment: "Data de atualização do registro",
                oldClrType: typeof(DateTime),
                oldType: "datetime(0)",
                oldNullable: true,
                oldComment: "Data de atualização do registro");

            migrationBuilder.AlterColumn<bool>(
                name: "deleted",
                table: "cash_movement",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                comment: "registro deletado virtualmente",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "registro deletado virtualmente");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "cash_movement",
                type: "datetime(0)",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                comment: "Data de criação do registro",
                oldClrType: typeof(DateTime),
                oldType: "datetime(0)",
                oldComment: "Data de criação do registro");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "cash_closing_terminal",
                type: "datetime(0)",
                nullable: true,
                defaultValueSql: "CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP",
                comment: "Data de atualização do registro",
                oldClrType: typeof(DateTime),
                oldType: "datetime(0)",
                oldNullable: true,
                oldComment: "Data de atualização do registro");

            migrationBuilder.AlterColumn<bool>(
                name: "deleted",
                table: "cash_closing_terminal",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                comment: "registro deletado virtualmente",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "registro deletado virtualmente");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "cash_closing_terminal",
                type: "datetime(0)",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                comment: "Data de criação do registro",
                oldClrType: typeof(DateTime),
                oldType: "datetime(0)",
                oldComment: "Data de criação do registro");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "cash_closing",
                type: "datetime(0)",
                nullable: true,
                defaultValueSql: "CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP",
                comment: "Data de atualização do registro",
                oldClrType: typeof(DateTime),
                oldType: "datetime(0)",
                oldNullable: true,
                oldComment: "Data de atualização do registro");

            migrationBuilder.AlterColumn<bool>(
                name: "deleted",
                table: "cash_closing",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                comment: "registro deletado virtualmente",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "registro deletado virtualmente");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "cash_closing",
                type: "datetime(0)",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                comment: "Data de criação do registro",
                oldClrType: typeof(DateTime),
                oldType: "datetime(0)",
                oldComment: "Data de criação do registro");

            migrationBuilder.Sql(@"
INSERT INTO payment_provider (name, logo_icon) values
('InfinitePay', 'bank-icons/infinitepay.webp'), ('Stone', 'bank-icons/stone.png');");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "payment_terminal",
                type: "datetime(0)",
                nullable: true,
                comment: "Data de atualização do registro",
                oldClrType: typeof(DateTime),
                oldType: "datetime(0)",
                oldNullable: true,
                oldDefaultValueSql: "CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP",
                oldComment: "Data de atualização do registro");

            migrationBuilder.AlterColumn<bool>(
                name: "deleted",
                table: "payment_terminal",
                type: "tinyint(1)",
                nullable: false,
                comment: "registro deletado virtualmente",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldDefaultValue: false,
                oldComment: "registro deletado virtualmente");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "payment_terminal",
                type: "datetime(0)",
                nullable: false,
                comment: "Data de criação do registro",
                oldClrType: typeof(DateTime),
                oldType: "datetime(0)",
                oldDefaultValueSql: "CURRENT_TIMESTAMP",
                oldComment: "Data de criação do registro");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "payment_provider",
                type: "datetime(0)",
                nullable: true,
                comment: "Data de atualização do registro",
                oldClrType: typeof(DateTime),
                oldType: "datetime(0)",
                oldNullable: true,
                oldDefaultValueSql: "CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP",
                oldComment: "Data de atualização do registro");

            migrationBuilder.AlterColumn<bool>(
                name: "deleted",
                table: "payment_provider",
                type: "tinyint(1)",
                nullable: false,
                comment: "registro deletado virtualmente",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldDefaultValue: false,
                oldComment: "registro deletado virtualmente");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "payment_provider",
                type: "datetime(0)",
                nullable: false,
                comment: "Data de criação do registro",
                oldClrType: typeof(DateTime),
                oldType: "datetime(0)",
                oldDefaultValueSql: "CURRENT_TIMESTAMP",
                oldComment: "Data de criação do registro");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "cash_register_session",
                type: "datetime(0)",
                nullable: true,
                comment: "Data de atualização do registro",
                oldClrType: typeof(DateTime),
                oldType: "datetime(0)",
                oldNullable: true,
                oldDefaultValueSql: "CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP",
                oldComment: "Data de atualização do registro");

            migrationBuilder.AlterColumn<bool>(
                name: "deleted",
                table: "cash_register_session",
                type: "tinyint(1)",
                nullable: false,
                comment: "registro deletado virtualmente",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldDefaultValue: false,
                oldComment: "registro deletado virtualmente");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "cash_register_session",
                type: "datetime(0)",
                nullable: false,
                comment: "Data de criação do registro",
                oldClrType: typeof(DateTime),
                oldType: "datetime(0)",
                oldDefaultValueSql: "CURRENT_TIMESTAMP",
                oldComment: "Data de criação do registro");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "cash_movement",
                type: "datetime(0)",
                nullable: true,
                comment: "Data de atualização do registro",
                oldClrType: typeof(DateTime),
                oldType: "datetime(0)",
                oldNullable: true,
                oldDefaultValueSql: "CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP",
                oldComment: "Data de atualização do registro");

            migrationBuilder.AlterColumn<bool>(
                name: "deleted",
                table: "cash_movement",
                type: "tinyint(1)",
                nullable: false,
                comment: "registro deletado virtualmente",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldDefaultValue: false,
                oldComment: "registro deletado virtualmente");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "cash_movement",
                type: "datetime(0)",
                nullable: false,
                comment: "Data de criação do registro",
                oldClrType: typeof(DateTime),
                oldType: "datetime(0)",
                oldDefaultValueSql: "CURRENT_TIMESTAMP",
                oldComment: "Data de criação do registro");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "cash_closing_terminal",
                type: "datetime(0)",
                nullable: true,
                comment: "Data de atualização do registro",
                oldClrType: typeof(DateTime),
                oldType: "datetime(0)",
                oldNullable: true,
                oldDefaultValueSql: "CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP",
                oldComment: "Data de atualização do registro");

            migrationBuilder.AlterColumn<bool>(
                name: "deleted",
                table: "cash_closing_terminal",
                type: "tinyint(1)",
                nullable: false,
                comment: "registro deletado virtualmente",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldDefaultValue: false,
                oldComment: "registro deletado virtualmente");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "cash_closing_terminal",
                type: "datetime(0)",
                nullable: false,
                comment: "Data de criação do registro",
                oldClrType: typeof(DateTime),
                oldType: "datetime(0)",
                oldDefaultValueSql: "CURRENT_TIMESTAMP",
                oldComment: "Data de criação do registro");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "cash_closing",
                type: "datetime(0)",
                nullable: true,
                comment: "Data de atualização do registro",
                oldClrType: typeof(DateTime),
                oldType: "datetime(0)",
                oldNullable: true,
                oldDefaultValueSql: "CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP",
                oldComment: "Data de atualização do registro");

            migrationBuilder.AlterColumn<bool>(
                name: "deleted",
                table: "cash_closing",
                type: "tinyint(1)",
                nullable: false,
                comment: "registro deletado virtualmente",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldDefaultValue: false,
                oldComment: "registro deletado virtualmente");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "cash_closing",
                type: "datetime(0)",
                nullable: false,
                comment: "Data de criação do registro",
                oldClrType: typeof(DateTime),
                oldType: "datetime(0)",
                oldDefaultValueSql: "CURRENT_TIMESTAMP",
                oldComment: "Data de criação do registro");
        }
    }
}
