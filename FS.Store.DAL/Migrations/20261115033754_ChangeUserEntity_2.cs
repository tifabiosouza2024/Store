using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FS.Store.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserEntity_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "accepted_at",
                table: "operation_user_role",
                type: "datetime(0)",
                nullable: true,
                comment: "Data de aceitação do convite");

            migrationBuilder.AddColumn<DateTime>(
                name: "invited_at",
                table: "operation_user_role",
                type: "datetime(0)",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                comment: "Data do convite");

            migrationBuilder.AddColumn<Guid>(
                name: "invited_by_user_id",
                table: "operation_user_role",
                type: "char(36)",
                nullable: true,
                comment: "Usuário responsável pelo convite");

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "operation_user_role",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Status do vínculo do usuário com a operação");

            migrationBuilder.CreateIndex(
                name: "IX_operation_user_role_invited_by_user_id",
                table: "operation_user_role",
                column: "invited_by_user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_operation_user_role_users_invited_by_user_id",
                table: "operation_user_role",
                column: "invited_by_user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_operation_user_role_users_invited_by_user_id",
                table: "operation_user_role");

            migrationBuilder.DropIndex(
                name: "IX_operation_user_role_invited_by_user_id",
                table: "operation_user_role");

            migrationBuilder.DropColumn(
                name: "accepted_at",
                table: "operation_user_role");

            migrationBuilder.DropColumn(
                name: "invited_at",
                table: "operation_user_role");

            migrationBuilder.DropColumn(
                name: "invited_by_user_id",
                table: "operation_user_role");

            migrationBuilder.DropColumn(
                name: "status",
                table: "operation_user_role");
        }
    }
}
