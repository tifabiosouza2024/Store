using Microsoft.EntityFrameworkCore;

namespace FS.Store.Model.Enums
{
    /// <summary>
    /// Tipo de movimentação do caixa sendo:</br>
    /// Suprimento: Entrada em dinheiro que não é oriunda de venda, como aporte para troco.</br>
    /// Sangria: Retirada de dinheiro que nao esta relacionado a despesas.</br>
    /// Despesas: Retirada de dinheiro relacionado a despesas.
    /// </summary>
    public enum CashMovementTypeEnum
    {
        [Comment("Suprimento (Entrada de dinheiro)")]
        Supply = 0,
        [Comment("Sangria")]
        Withdrawal = 1,
        [Comment("Despesa")]
        Expense = 2
    }
}
