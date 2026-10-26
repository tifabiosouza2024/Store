using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FS.Store.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "users",
                comment: "Usuários do sistema",
                oldComment: "Usuarios do sistema")
                .Annotation("MySQL:Charset", "utf8mb4")
                .OldAnnotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.AlterTable(
                name: "roles",
                comment: "Papéis de usuários do sistema",
                oldComment: "Papeis de usuarios do sistema")
                .Annotation("MySQL:Charset", "utf8mb4")
                .OldAnnotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.AlterTable(
                name: "operation_user_role",
                comment: "Relacionamento entre Operação, Usuário e seus papeis",
                oldComment: "Relacionamento entre operacao, usuario e seus papeis")
                .Annotation("MySQL:Charset", "utf8mb4")
                .OldAnnotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "remember_me",
                table: "users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                comment: "Informa se o usuário permanece logado após fechar o navegador",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldDefaultValue: false,
                oldComment: "Informa se o usuario permanece logado apos fechar o navegador");

            migrationBuilder.AlterColumn<string>(
                name: "password_hash",
                table: "users",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                comment: "Hash de senha",
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255,
                oldComment: "Hash de senha do usuario");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "users",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                comment: "Nome do usuário",
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255,
                oldComment: "Nome do usuario");

            migrationBuilder.AlterColumn<Guid>(
                name: "last_operation_id",
                table: "users",
                type: "char(36)",
                nullable: true,
                comment: "Id da última operação logada",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true,
                oldComment: "Id da ultima operacao logada");

            migrationBuilder.AlterColumn<bool>(
                name: "is_beta",
                table: "users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                comment: "Usuário testador de versões beta",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldDefaultValue: false,
                oldComment: "Usuario testador de versoes beta");

            migrationBuilder.AlterColumn<bool>(
                name: "is_active",
                table: "users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                comment: "Usuário ativo",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldDefaultValue: false,
                oldComment: "Usuario ativo");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "users",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                comment: "E-mail do usuário",
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255,
                oldComment: "E-mail do usuario");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "operations",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                comment: "Nome da Operação",
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255,
                oldComment: "Nome da operacao");

            migrationBuilder.AlterColumn<bool>(
                name: "is_active",
                table: "operations",
                type: "tinyint(1)",
                nullable: true,
                defaultValue: true,
                comment: "Operação ativa",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldNullable: true,
                oldDefaultValue: true,
                oldComment: "Operacao ativa");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "operations",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                comment: "E-mail da Operação",
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255,
                oldComment: "E-mail da operacao");

            migrationBuilder.AlterColumn<string>(
                name: "document",
                table: "operations",
                type: "varchar(18)",
                maxLength: 18,
                nullable: false,
                comment: "CNPJ da Operação",
                oldClrType: typeof(string),
                oldType: "varchar(18)",
                oldMaxLength: 18,
                oldComment: "CNPJ da operacao");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "operation_user_role",
                type: "char(36)",
                nullable: false,
                comment: "Id do Usuário",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldComment: "Id do usuario");

            migrationBuilder.AlterColumn<int>(
                name: "role_id",
                table: "operation_user_role",
                type: "int",
                nullable: false,
                comment: "Id do papel do Usuário na Operação",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Id da funcao de acesso");

            migrationBuilder.AlterColumn<Guid>(
                name: "operation_id",
                table: "operation_user_role",
                type: "char(36)",
                nullable: false,
                comment: "Id da Operação",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldComment: "Id da operacao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "users",
                comment: "Usuarios do sistema",
                oldComment: "Usuários do sistema")
                .Annotation("MySQL:Charset", "utf8mb4")
                .OldAnnotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.AlterTable(
                name: "roles",
                comment: "Papeis de usuarios do sistema",
                oldComment: "Papéis de usuários do sistema")
                .Annotation("MySQL:Charset", "utf8mb4")
                .OldAnnotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.AlterTable(
                name: "operation_user_role",
                comment: "Relacionamento entre operacao, usuario e seus papeis",
                oldComment: "Relacionamento entre Operação, Usuário e seus papeis")
                .Annotation("MySQL:Charset", "utf8mb4")
                .OldAnnotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "remember_me",
                table: "users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                comment: "Informa se o usuario permanece logado apos fechar o navegador",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldDefaultValue: false,
                oldComment: "Informa se o usuário permanece logado após fechar o navegador");

            migrationBuilder.AlterColumn<string>(
                name: "password_hash",
                table: "users",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                comment: "Hash de senha do usuario",
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255,
                oldComment: "Hash de senha");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "users",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                comment: "Nome do usuario",
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255,
                oldComment: "Nome do usuário");

            migrationBuilder.AlterColumn<Guid>(
                name: "last_operation_id",
                table: "users",
                type: "char(36)",
                nullable: true,
                comment: "Id da ultima operacao logada",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true,
                oldComment: "Id da última operação logada");

            migrationBuilder.AlterColumn<bool>(
                name: "is_beta",
                table: "users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                comment: "Usuario testador de versoes beta",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldDefaultValue: false,
                oldComment: "Usuário testador de versões beta");

            migrationBuilder.AlterColumn<bool>(
                name: "is_active",
                table: "users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                comment: "Usuario ativo",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldDefaultValue: false,
                oldComment: "Usuário ativo");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "users",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                comment: "E-mail do usuario",
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255,
                oldComment: "E-mail do usuário");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "operations",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                comment: "Nome da operacao",
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255,
                oldComment: "Nome da Operação");

            migrationBuilder.AlterColumn<bool>(
                name: "is_active",
                table: "operations",
                type: "tinyint(1)",
                nullable: true,
                defaultValue: true,
                comment: "Operacao ativa",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldNullable: true,
                oldDefaultValue: true,
                oldComment: "Operação ativa");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "operations",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                comment: "E-mail da operacao",
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255,
                oldComment: "E-mail da Operação");

            migrationBuilder.AlterColumn<string>(
                name: "document",
                table: "operations",
                type: "varchar(18)",
                maxLength: 18,
                nullable: false,
                comment: "CNPJ da operacao",
                oldClrType: typeof(string),
                oldType: "varchar(18)",
                oldMaxLength: 18,
                oldComment: "CNPJ da Operação");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "operation_user_role",
                type: "char(36)",
                nullable: false,
                comment: "Id do usuario",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldComment: "Id do Usuário");

            migrationBuilder.AlterColumn<int>(
                name: "role_id",
                table: "operation_user_role",
                type: "int",
                nullable: false,
                comment: "Id da funcao de acesso",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Id do papel do Usuário na Operação");

            migrationBuilder.AlterColumn<Guid>(
                name: "operation_id",
                table: "operation_user_role",
                type: "char(36)",
                nullable: false,
                comment: "Id da operacao",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldComment: "Id da Operação");
        }
    }
}
