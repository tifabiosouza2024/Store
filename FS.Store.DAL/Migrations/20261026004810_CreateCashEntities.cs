using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace FS.Store.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CreateCashEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cash_register_session",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    opened_at = table.Column<DateTime>(type: "datetime(0)", nullable: false, comment: "Data/Hora de abertura"),
                    closed_at = table.Column<DateTime>(type: "datetime(0)", nullable: true, comment: "Data/Hora de fechamento"),
                    opening_amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false, comment: "Valor em espécie do total da abertura de caixa"),
                    cash_register_status = table.Column<int>(type: "int", nullable: false, comment: "Status da abertura de caixa"),
                    operation_id = table.Column<Guid>(type: "char(36)", nullable: false, comment: "ID da Operação"),
                    opened_by_user_id = table.Column<Guid>(type: "char(36)", nullable: false, comment: "ID do Usuário que abriu o caixa"),
                    closed_by_user_id = table.Column<Guid>(type: "char(36)", nullable: false, comment: "ID do Usuário que fechou o caixa"),
                    created_at = table.Column<DateTime>(type: "datetime(0)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP", comment: "Data de criação do registro"),
                    updated_at = table.Column<DateTime>(type: "datetime(0)", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP", comment: "Data de atualização do registro"),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false, comment: "registro deletado virtualmente")
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
                        name: "FK_cash_register_session_users_closed_by_user_id",
                        column: x => x.closed_by_user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "provider",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "Empresa da maquininha de cartão"),
                    created_at = table.Column<DateTime>(type: "datetime(0)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP", comment: "Data de criação do registro"),
                    updated_at = table.Column<DateTime>(type: "datetime(0)", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP", comment: "Data de atualização do registro"),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false, comment: "registro deletado virtualmente")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_provider", x => x.id);
                },
                comment: "Armazena os fornecedores dos terminais")
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cash_closing",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    closed_at = table.Column<DateTime>(type: "datetime(0)", nullable: false, comment: "Data/Hora de fechamento"),
                    cash_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Valor total em dinheiro"),
                    total_credit = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Valor total de crédito"),
                    total_debit = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Valor total de débito"),
                    total_pix = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Valor total de PIX"),
                    notes = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, comment: "Observações sobre o fechamento"),
                    cash_register_session_id = table.Column<long>(type: "bigint", nullable: false, comment: "Id da sessão de Caixa"),
                    closed_by_user_id = table.Column<Guid>(type: "char(36)", nullable: false, comment: "Usuário que realizou o fechamento"),
                    created_at = table.Column<DateTime>(type: "datetime(0)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP", comment: "Data de criação do registro"),
                    updated_at = table.Column<DateTime>(type: "datetime(0)", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP", comment: "Data de atualização do registro"),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false, comment: "registro deletado virtualmente")
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
                    cash_movement_type = table.Column<int>(type: "int", maxLength: 20, nullable: false, comment: "Tipo de movimentação (suprimento, sangria, despesa)"),
                    amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false, comment: "valor da movimentação"),
                    description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "Descrição"),
                    occurred_at = table.Column<DateTime>(type: "datetime(0)", nullable: false, comment: "Data/Hora em que ocorreu a movimentação"),
                    canceled_at = table.Column<DateTime>(type: "datetime(0)", nullable: false, comment: "Data/Hora em que houve o cancelamento da movimentação"),
                    cancellation_reason = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, comment: "Motivo do cancelamento"),
                    created_by_user_id = table.Column<Guid>(type: "char(36)", nullable: false, comment: "Usuário que criou a movimentação"),
                    canceled_by_user_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "Usuário que realizou o cancelamento da movimentação"),
                    cash_register_session_id = table.Column<long>(type: "bigint", nullable: false, comment: "Sessão de Caixa"),
                    created_at = table.Column<DateTime>(type: "datetime(0)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP", comment: "Data de criação do registro"),
                    updated_at = table.Column<DateTime>(type: "datetime(0)", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP", comment: "Data de atualização do registro"),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false, comment: "registro deletado virtualmente")
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
                name: "payment_terminal",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "Nome do terminal"),
                    payment_provider_id = table.Column<int>(type: "int", nullable: false, comment: "Id do fornecedor do terminal"),
                    operation_id = table.Column<Guid>(type: "char(36)", nullable: false, comment: "Id da operação"),
                    created_at = table.Column<DateTime>(type: "datetime(0)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP", comment: "Data de criação do registro"),
                    updated_at = table.Column<DateTime>(type: "datetime(0)", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP", comment: "Data de atualização do registro"),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false, comment: "registro deletado virtualmente")
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
                        name: "FK_payment_terminal_provider_payment_provider_id",
                        column: x => x.payment_provider_id,
                        principalTable: "provider",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Armazena os terminais (maquininhas) de pagamento")
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cash_closing_terminal",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    credit_amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false, comment: "Valor em crédito do terminal"),
                    debit_amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false, comment: "Valor em débito do terminal"),
                    pix_amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false, comment: "Valor em PIX do terminal"),
                    payment_terminal_id = table.Column<Guid>(type: "char(36)", nullable: false, comment: "Terminal de pagamento (maquininha)"),
                    cash_register_session_id = table.Column<long>(type: "bigint", nullable: false),
                    created_by_user_id = table.Column<Guid>(type: "char(36)", nullable: false, comment: "Usuário que criou a movimentação"),
                    created_at = table.Column<DateTime>(type: "datetime(0)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP", comment: "Data de criação do registro"),
                    updated_at = table.Column<DateTime>(type: "datetime(0)", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP", comment: "Data de atualização do registro"),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false, comment: "registro deletado virtualmente")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cash_closing_terminal", x => x.id);
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
                column: "cash_register_session_id");

            migrationBuilder.CreateIndex(
                name: "IX_cash_closing_closed_by_user_id",
                table: "cash_closing",
                column: "closed_by_user_id");

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
                name: "IX_payment_terminal_operation_id",
                table: "payment_terminal",
                column: "operation_id");

            migrationBuilder.CreateIndex(
                name: "IX_payment_terminal_payment_provider_id",
                table: "payment_terminal",
                column: "payment_provider_id");

            migrationBuilder.Sql(@"
INSERT INTO provider (name) VALUES 
('Stone'),
('InfinitePay');");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cash_closing");

            migrationBuilder.DropTable(
                name: "cash_closing_terminal");

            migrationBuilder.DropTable(
                name: "cash_movement");

            migrationBuilder.DropTable(
                name: "payment_terminal");

            migrationBuilder.DropTable(
                name: "cash_register_session");

            migrationBuilder.DropTable(
                name: "provider");
        }
    }
}
