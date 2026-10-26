using FS.Store.Model.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace FS.Store.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CreateUserAndRoleAndOperationAndOperationUserRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "operations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false),
                    document = table.Column<string>(type: "varchar(18)", maxLength: 18, nullable: false, comment: "CNPJ da operacao"),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "Nome da operacao"),
                    email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "E-mail da operacao"),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: true, defaultValue: true, comment: "Operacao ativa"),
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
                comment: "Papeis de usuarios do sistema")
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false),
                    email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "E-mail do usuario"),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "Nome do usuario"),
                    password_hash = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "Hash de senha do usuario"),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false, comment: "Usuario ativo"),
                    is_beta = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false, comment: "Usuario testador de versoes beta"),
                    remember_me = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false, comment: "Informa se o usuario permanece logado apos fechar o navegador"),
                    last_operation_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "Id da ultima operacao logada"),
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
                comment: "Usuarios do sistema")
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "operation_user_role",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    operation_id = table.Column<Guid>(type: "char(36)", nullable: false, comment: "Id da operacao"),
                    user_id = table.Column<Guid>(type: "char(36)", nullable: false, comment: "Id do usuario"),
                    role_id = table.Column<int>(type: "int", nullable: false, comment: "Id da funcao de acesso"),
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
                        name: "FK_operation_user_role_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Relacionamento entre operacao, usuario e seus papeis")
                .Annotation("MySQL:Charset", "utf8mb4");

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

            var passwordHasher = new PasswordHasher<User>();
            var u = new User();
            var passwordHashed = passwordHasher.HashPassword(u, "user123!");
            migrationBuilder.Sql("INSERT INTO roles (name, normalized_name, deleted) VALUES ('Super', 'SUPER', false);");
            migrationBuilder.Sql("INSERT INTO roles (name, normalized_name, deleted) VALUES ('Admin', 'ADMIN', false);");
            migrationBuilder.Sql("INSERT INTO roles (name, normalized_name, deleted) VALUES ('Manager', 'MANAGER', false);");
            migrationBuilder.Sql("iNSERT INTO roles (name, normalized_name, deleted) VALUES ('Seller', 'SELLER', false);");
            migrationBuilder.Sql($"INSERT INTO users (id, name, email, password_hash, is_active, is_beta) VALUES (uuid(),'Super User', 'superuser@gmail.com', '{passwordHashed}', true, true);");
            migrationBuilder.Sql($"INSERT INTO users (id, name, email, password_hash, is_active, is_beta) VALUES (uuid(),'Seller User', 'selleruser@gmail.com', '{passwordHashed}', true, true);");
            migrationBuilder.Sql($"INSERT INTO operations (id, document, name, email) VALUES (uuid(),'40.953.557/0001-61', 'First Gift', 'firstgift@mail.com');");
            migrationBuilder.Sql($"INSERT INTO operations (id, document, name, email) VALUES (uuid(),'67.192.514/0001-67', 'Seccond Gift', 'seccondgift@mail.com');");
            migrationBuilder.Sql(@"
INSERT INTO operation_user_role (user_id, role_id, operation_id)
SELECT
    (SELECT id FROM users WHERE email like 'super%'), r.id, o.id
FROM roles r
CROSS JOIN operations o
WHERE r.normalized_name = 'SUPER';");
            migrationBuilder.Sql(@"
INSERT INTO operation_user_role (user_id, role_id, operation_id)
SELECT
    (SELECT id FROM users WHERE email like 'seller%'), r.id, o.id
FROM roles r
CROSS JOIN operations o
WHERE r.normalized_name = 'SELLER';");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "operation_user_role");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "operations");
        }
    }
}
