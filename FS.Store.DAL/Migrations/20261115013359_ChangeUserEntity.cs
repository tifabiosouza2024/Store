using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FS.Store.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "is_active",
                table: "users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: true,
                comment: "Usuário ativo",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldDefaultValue: false,
                oldComment: "Usuário ativo");

            migrationBuilder.AddColumn<bool>(
                name: "must_change_password",
                table: "users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: true,
                comment: "Indica se o usuário deve alterar a senha no próximo acesso");

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "operation_user_role",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: true,
                comment: "Determina se o usuário estará ativo para aquela operação.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "must_change_password",
                table: "users");

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "operation_user_role");

            migrationBuilder.AlterColumn<bool>(
                name: "is_active",
                table: "users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                comment: "Usuário ativo",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldDefaultValue: true,
                oldComment: "Usuário ativo");
        }
    }
}
