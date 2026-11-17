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
                name: "payment_provider",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "Empresa da maquininha de cartão"),
                    logo_icon = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, comment: "Ícone do logo do fornecedor"),
                    created_at = table.Column<DateTime>(type: "datetime(0)", nullable: false, comment: "Data de criação do registro"),
                    updated_at = table.Column<DateTime>(type: "datetime(0)", nullable: true, comment: "Data de atualização do registro"),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "registro deletado virtualmente")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment_provider", x => x.id);
                },
                comment: "Armazena os fornecedores dos terminais")
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
                comment: "Papéis de usuários do sistema")
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
                name: "payment_terminal",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "Nome do terminal"),
                    payment_provider_id = table.Column<int>(type: "int", nullable: false, comment: "Id do fornecedor do terminal"),
                    operation_id = table.Column<Guid>(type: "char(36)", nullable: false, comment: "Id da operação"),
                    created_at = table.Column<DateTime>(type: "datetime(0)", nullable: false, comment: "Data de criação do registro"),
                    updated_at = table.Column<DateTime>(type: "datetime(0)", nullable: true, comment: "Data de atualização do registro"),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "registro deletado virtualmente")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment_terminal", x => x.id);
                    table.ForeignKey(
                        name: "FK_payment_terminal_operations_operation_id",
                        column: x => x.operation_id,
                        principalTable: "operations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_payment_terminal_payment_provider_payment_provider_id",
                        column: x => x.payment_provider_id,
                        principalTable: "payment_provider",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Armazena os terminais (maquininhas) de pagamento")
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cash_register_session",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    opened_at = table.Column<DateTime>(type: "datetime(0)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP", comment: "Data/Hora de abertura"),
                    closed_at = table.Column<DateTime>(type: "datetime(0)", nullable: true, comment: "Data/Hora de fechamento"),
                    opening_amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false, comment: "Valor em espécie do total da abertura de caixa"),
                    total_sales = table.Column<decimal>(type: "decimal(10,2)", nullable: false, comment: "Valor total vendido"),
                    cash_register_status = table.Column<int>(type: "int", nullable: false, comment: "Status da abertura de caixa (1 - aberto, 2 - fechado, 3 - cancelado)"),
                    cash_session_number = table.Column<int>(type: "int", nullable: false),
                    operation_id = table.Column<Guid>(type: "char(36)", nullable: false, comment: "ID da Operação"),
                    opened_by_user_id = table.Column<Guid>(type: "char(36)", nullable: false, comment: "ID do Usuário que abriu o caixa"),
                    closed_by_user_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "ID do Usuário que fechou o caixa"),
                    changed_by_user_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "ID do Usuário que alterou o caixa"),
                    created_at = table.Column<DateTime>(type: "datetime(0)", nullable: false, comment: "Data de criação do registro"),
                    updated_at = table.Column<DateTime>(type: "datetime(0)", nullable: true, comment: "Data de atualização do registro"),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "registro deletado virtualmente")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cash_register_session", x => x.id);
                    table.ForeignKey(
                        name: "FK_cash_register_session_operations_operation_id",
                        column: x => x.operation_id,
                        principalTable: "operations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cash_register_session_users_changed_by_user_id",
                        column: x => x.changed_by_user_id,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_cash_register_session_users_closed_by_user_id",
                        column: x => x.closed_by_user_id,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_cash_register_session_users_opened_by_user_id",
                        column: x => x.opened_by_user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "controle de abertura e fechamento de caixa")
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
                comment: "Relacionamento entre Operação, Usuário e seus papeis")
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cash_closing",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    closed_at = table.Column<DateTime>(type: "datetime(0)", nullable: false, comment: "Data/Hora de fechamento"),
                    cash_closing_amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false, comment: "Valor total em dinheiro encontrado no fechamento do caixa"),
                    total_credit = table.Column<decimal>(type: "decimal(10,2)", nullable: false, comment: "Valor total de crédito passado em todas as maquininhas"),
                    total_debit = table.Column<decimal>(type: "decimal(10,2)", nullable: false, comment: "Valor total de débito passado em todas as maquininhas"),
                    total_pix = table.Column<decimal>(type: "decimal(10,2)", nullable: false, comment: "Valor total de PIX recebido em todas as maquininhas"),
                    total_expenses = table.Column<decimal>(type: "decimal(10,2)", nullable: false, comment: "Valor total de despesas pago em dinheiro"),
                    total_withdrawal = table.Column<decimal>(type: "decimal(10,2)", nullable: false, comment: "Valor total de saques realizado em dinheiro"),
                    total_cash_inflows = table.Column<decimal>(type: "decimal(10,2)", nullable: false, comment: "Valor total de aportes em dinheiro"),
                    notes = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, comment: "Observações sobre o fechamento"),
                    cash_register_session_id = table.Column<long>(type: "bigint", nullable: false, comment: "Id da sessão de Caixa"),
                    closed_by_user_id = table.Column<Guid>(type: "char(36)", nullable: false, comment: "Usuário que realizou o fechamento"),
                    created_at = table.Column<DateTime>(type: "datetime(0)", nullable: false, comment: "Data de criação do registro"),
                    updated_at = table.Column<DateTime>(type: "datetime(0)", nullable: true, comment: "Data de atualização do registro"),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "registro deletado virtualmente")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cash_closing", x => x.id);
                    table.ForeignKey(
                        name: "FK_cash_closing_cash_register_session_cash_register_session_id",
                        column: x => x.cash_register_session_id,
                        principalTable: "cash_register_session",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cash_closing_users_closed_by_user_id",
                        column: x => x.closed_by_user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Registra o fechamento do caixa")
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cash_movement",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    cash_movement_type = table.Column<int>(type: "int", nullable: false, comment: "Tipo de movimentação (1 - suprimento, 2 - sangria, 3 - despesa)"),
                    amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false, comment: "valor da movimentação"),
                    description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, comment: "Descrição"),
                    occurred_at = table.Column<DateTime>(type: "datetime(0)", nullable: false, comment: "Data/Hora em que ocorreu a movimentação"),
                    canceled_at = table.Column<DateTime>(type: "datetime(0)", nullable: true, comment: "Data/Hora em que houve o cancelamento da movimentação"),
                    cancellation_reason = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, comment: "Motivo do cancelamento"),
                    created_by_user_id = table.Column<Guid>(type: "char(36)", nullable: false, comment: "Usuário que criou a movimentação"),
                    canceled_by_user_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "Usuário que realizou o cancelamento da movimentação"),
                    cash_register_session_id = table.Column<long>(type: "bigint", nullable: false, comment: "Sessão de Caixa"),
                    created_at = table.Column<DateTime>(type: "datetime(0)", nullable: false, comment: "Data de criação do registro"),
                    updated_at = table.Column<DateTime>(type: "datetime(0)", nullable: true, comment: "Data de atualização do registro"),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "registro deletado virtualmente")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cash_movement", x => x.id);
                    table.ForeignKey(
                        name: "FK_cash_movement_cash_register_session_cash_register_session_id",
                        column: x => x.cash_register_session_id,
                        principalTable: "cash_register_session",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cash_movement_users_canceled_by_user_id",
                        column: x => x.canceled_by_user_id,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_cash_movement_users_created_by_user_id",
                        column: x => x.created_by_user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Registra as movimentações financeiras")
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cash_closing_terminal",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    closed_at = table.Column<DateTime>(type: "datetime(0)", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP", comment: "Data/Hora de fechamento"),
                    credit_amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false, comment: "Valor em crédito do terminal"),
                    debit_amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false, comment: "Valor em débito do terminal"),
                    pix_amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false, comment: "Valor em PIX do terminal"),
                    payment_terminal_id = table.Column<Guid>(type: "char(36)", nullable: false, comment: "Terminal de pagamento (maquininha)"),
                    cash_register_session_id = table.Column<long>(type: "bigint", nullable: false),
                    cash_closing_id = table.Column<long>(type: "bigint", nullable: false),
                    created_by_user_id = table.Column<Guid>(type: "char(36)", nullable: false, comment: "Usuário que criou a movimentação"),
                    created_at = table.Column<DateTime>(type: "datetime(0)", nullable: false, comment: "Data de criação do registro"),
                    updated_at = table.Column<DateTime>(type: "datetime(0)", nullable: true, comment: "Data de atualização do registro"),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "registro deletado virtualmente")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cash_closing_terminal", x => x.id);
                    table.ForeignKey(
                        name: "FK_cash_closing_terminal_cash_closing_cash_closing_id",
                        column: x => x.cash_closing_id,
                        principalTable: "cash_closing",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cash_closing_terminal_cash_register_session_cash_register_se~",
                        column: x => x.cash_register_session_id,
                        principalTable: "cash_register_session",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cash_closing_terminal_payment_terminal_payment_terminal_id",
                        column: x => x.payment_terminal_id,
                        principalTable: "payment_terminal",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cash_closing_terminal_users_created_by_user_id",
                        column: x => x.created_by_user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Armazena o valor de cada terminal (maquininha)")
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_cash_closing_cash_register_session_id",
                table: "cash_closing",
                column: "cash_register_session_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cash_closing_closed_by_user_id",
                table: "cash_closing",
                column: "closed_by_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_cash_closing_terminal_cash_closing_id",
                table: "cash_closing_terminal",
                column: "cash_closing_id");

            migrationBuilder.CreateIndex(
                name: "IX_cash_closing_terminal_cash_register_session_id",
                table: "cash_closing_terminal",
                column: "cash_register_session_id");

            migrationBuilder.CreateIndex(
                name: "IX_cash_closing_terminal_created_by_user_id",
                table: "cash_closing_terminal",
                column: "created_by_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_cash_closing_terminal_payment_terminal_id",
                table: "cash_closing_terminal",
                column: "payment_terminal_id");

            migrationBuilder.CreateIndex(
                name: "IX_cash_movement_canceled_by_user_id",
                table: "cash_movement",
                column: "canceled_by_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_cash_movement_cash_register_session_id",
                table: "cash_movement",
                column: "cash_register_session_id");

            migrationBuilder.CreateIndex(
                name: "IX_cash_movement_created_by_user_id",
                table: "cash_movement",
                column: "created_by_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_cash_register_session_changed_by_user_id",
                table: "cash_register_session",
                column: "changed_by_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_cash_register_session_closed_by_user_id",
                table: "cash_register_session",
                column: "closed_by_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_cash_register_session_opened_by_user_id",
                table: "cash_register_session",
                column: "opened_by_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_cash_register_session_operation_id",
                table: "cash_register_session",
                column: "operation_id");

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
                name: "IX_payment_terminal_operation_id",
                table: "payment_terminal",
                column: "operation_id");

            migrationBuilder.CreateIndex(
                name: "IX_payment_terminal_payment_provider_id",
                table: "payment_terminal",
                column: "payment_provider_id");

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
INSERT INTO roles (name, normalized_name, deleted) VALUES
('Super', 'SUPER', false),
('Admin', 'ADMIN', false),
('Manager', 'MANAGER', false),
('Seller', 'SELLER', false);");

            var passwordHasher = new PasswordHasher<User>();
            var u = new User();
            var passwordHashed = passwordHasher.HashPassword(u, "admin@123!");
            migrationBuilder.Sql(@$"
INSERT INTO users (id, name, email, password_hash, is_active, is_beta) VALUES
(uuid(),'Super User', 'superuser@gmail.com', '{passwordHashed}', true, true);");

            migrationBuilder.Sql(@"
INSERT INTO operation_user_role (user_id, role_id, operation_id)
SELECT
    (SELECT id FROM users WHERE email LIKE 'superuser%'), r.id, o.id
FROM roles r
CROSS JOIN operations o
WHERE r.normalized_name = 'SUPER';");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cash_closing_terminal");

            migrationBuilder.DropTable(
                name: "cash_movement");

            migrationBuilder.DropTable(
                name: "operation_user_role");

            migrationBuilder.DropTable(
                name: "cash_closing");

            migrationBuilder.DropTable(
                name: "payment_terminal");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "cash_register_session");

            migrationBuilder.DropTable(
                name: "payment_provider");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "operations");
        }
    }
}
