using FS.Store.Model.Entity;
using FS.Store.Model.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace FS.Store.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CreateAccessManagement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "global_roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "Nome do papel"),
                    normalized_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "Nome do papel normalizado (em uppercase)"),
                    created_at = table.Column<DateTime>(type: "datetime(0)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP", comment: "Data de criação do registro"),
                    updated_at = table.Column<DateTime>(type: "datetime(0)", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP", comment: "Data de atualização do registro"),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false, comment: "registro deletado virtualmente")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_global_roles", x => x.id);
                },
                comment: "Papéis de usuários do sistema")
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "operations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false),
                    document = table.Column<string>(type: "varchar(18)", maxLength: 18, nullable: false, comment: "CNPJ da Operação"),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "Nome da Operação"),
                    email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "E-mail da Operação"),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: true, defaultValue: true, comment: "Operação ativa"),
                    created_at = table.Column<DateTime>(type: "datetime(0)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP", comment: "Data de criação do registro"),
                    updated_at = table.Column<DateTime>(type: "datetime(0)", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP", comment: "Data de atualização do registro"),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false, comment: "registro deletado virtualmente")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_operations", x => x.id);
                },
                comment: "Empresa/Loja Operante")
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "Nome do papel"),
                    normalized_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "Nome do papel normalizado (em uppercase)"),
                    created_at = table.Column<DateTime>(type: "datetime(0)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP", comment: "Data de criação do registro"),
                    updated_at = table.Column<DateTime>(type: "datetime(0)", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP", comment: "Data de atualização do registro"),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false, comment: "registro deletado virtualmente")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id);
                },
                comment: "Papéis de usuários das operações")
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false),
                    email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "E-mail do usuário"),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, comment: "Nome do usuário"),
                    password_hash = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "Hash de senha"),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true, comment: "Usuário ativo"),
                    is_beta = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false, comment: "Usuário testador de versões beta"),
                    remember_me = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false, comment: "Informa se o usuário permanece logado após fechar o navegador"),
                    must_change_password = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true, comment: "Indica se o usuário deve alterar a senha no próximo acesso"),
                    last_operation_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "Id da última operação logada"),
                    created_at = table.Column<DateTime>(type: "datetime(0)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP", comment: "Data de criação do registro"),
                    updated_at = table.Column<DateTime>(type: "datetime(0)", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP", comment: "Data de atualização do registro"),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false, comment: "registro deletado virtualmente")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                    table.ForeignKey(
                        name: "FK_users_operations_last_operation_id",
                        column: x => x.last_operation_id,
                        principalTable: "operations",
                        principalColumn: "id");
                },
                comment: "Usuários do sistema")
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "global_user_roles",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true, comment: "Determina se o usuário estará ativo para operação do sistema."),
                    status = table.Column<int>(type: "int", nullable: false, comment: "Status da função do usuário no sistema"),
                    invited_at = table.Column<DateTime>(type: "datetime(0)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP", comment: "Data do convite"),
                    accepted_at = table.Column<DateTime>(type: "datetime(0)", nullable: true, comment: "Data de aceitação do convite"),
                    user_id = table.Column<Guid>(type: "char(36)", nullable: false, comment: "Id do Usuário"),
                    role_id = table.Column<int>(type: "int", nullable: false, comment: "Id do papel do Usuário na Operação"),
                    invited_by_user_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "Usuário responsável pelo convite"),
                    created_at = table.Column<DateTime>(type: "datetime(0)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP", comment: "Data de criação do registro"),
                    updated_at = table.Column<DateTime>(type: "datetime(0)", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP", comment: "Data de atualização do registro"),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false, comment: "registro deletado virtualmente")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_global_user_roles", x => x.id);
                    table.ForeignKey(
                        name: "FK_global_user_roles_global_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "global_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_global_user_roles_users_invited_by_user_id",
                        column: x => x.invited_by_user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_global_user_roles_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Relacionamento entre usuário do sistema e suas funções")
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "operation_user_role",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true, comment: "Determina se o usuário estará ativo para aquela operação."),
                    status = table.Column<int>(type: "int", nullable: false, comment: "Status do vínculo do usuário com a operação"),
                    invited_at = table.Column<DateTime>(type: "datetime(0)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP", comment: "Data do convite"),
                    accepted_at = table.Column<DateTime>(type: "datetime(0)", nullable: true, comment: "Data de aceitação do convite"),
                    operation_id = table.Column<Guid>(type: "char(36)", nullable: false, comment: "Id da Operação"),
                    user_id = table.Column<Guid>(type: "char(36)", nullable: false, comment: "Id do Usuário"),
                    role_id = table.Column<int>(type: "int", nullable: false, comment: "Id do papel do Usuário na Operação"),
                    invited_by_user_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "Usuário responsável pelo convite"),
                    created_at = table.Column<DateTime>(type: "datetime(0)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP", comment: "Data de criação do registro"),
                    updated_at = table.Column<DateTime>(type: "datetime(0)", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP", comment: "Data de atualização do registro"),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false, comment: "registro deletado virtualmente")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_operation_user_role", x => x.id);
                    table.ForeignKey(
                        name: "FK_operation_user_role_operations_operation_id",
                        column: x => x.operation_id,
                        principalTable: "operations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_operation_user_role_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_operation_user_role_users_invited_by_user_id",
                        column: x => x.invited_by_user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_operation_user_role_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Relacionamento entre Operação, Usuário e seus papeis na operação")
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_global_user_roles_invited_by_user_id",
                table: "global_user_roles",
                column: "invited_by_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_global_user_roles_role_id",
                table: "global_user_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_global_user_roles_user_id_role_id",
                table: "global_user_roles",
                columns: new[] { "user_id", "role_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_operation_user_role_invited_by_user_id",
                table: "operation_user_role",
                column: "invited_by_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_operation_user_role_operation_id",
                table: "operation_user_role",
                column: "operation_id");

            migrationBuilder.CreateIndex(
                name: "IX_operation_user_role_role_id",
                table: "operation_user_role",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_operation_user_role_user_id_operation_id_role_id",
                table: "operation_user_role",
                columns: new[] { "user_id", "operation_id", "role_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_operations_document",
                table: "operations",
                column: "document",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_operations_document_email",
                table: "operations",
                columns: new[] { "document", "email" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_operations_email",
                table: "operations",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_last_operation_id",
                table: "users",
                column: "last_operation_id");

            migrationBuilder.Sql(@"
INSERT INTO global_roles (name, normalized_name, deleted) VALUES
('Super', 'SUPER', false);");

            migrationBuilder.Sql(@"
INSERT INTO roles (name, normalized_name, deleted) VALUES
('Admin', 'ADMIN', false),
('Manager', 'MANAGER', false),
('Seller', 'SELLER', false);");

            var passwordHasher = new PasswordHasher<User>();
            var u = new User();
            var passwordHashed = passwordHasher.HashPassword(u, "admin@123!");
            migrationBuilder.Sql(@$"
INSERT INTO users (id, name, email, password_hash, is_active, is_beta) VALUES
(uuid(),'Super User', 'superuser@mail.com', '{passwordHashed}', true, true);");

            migrationBuilder.Sql(@$"
INSERT INTO global_user_roles (user_id, role_id, is_active, status)
SELECT
    (SELECT id FROM users WHERE email = 'superuser@mail.com'), r.id, true, {(int)RoleStatusEnum.Active}
FROM global_roles r
WHERE r.normalized_name = 'SUPER';");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "global_user_roles");

            migrationBuilder.DropTable(
                name: "operation_user_role");

            migrationBuilder.DropTable(
                name: "global_roles");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "operations");
        }
    }
}
