using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FS.Store.Model.Enums
{
    /// <summary>
    /// Tipo de movimentação do caixa sendo:<br />
    /// 1 . Suprimento: Entrada em dinheiro que não é oriunda de venda, como aporte para troco.<br />
    /// 2 . Sangria: Retirada de dinheiro que nao esta relacionado a despesas.<br />
    /// 3. Despesas: Retirada de dinheiro relacionado a despesas.
    /// </summary>
    public enum CashMovementTypeEnum
    {
        [Comment("Suprimento (Entrada de dinheiro)")]
        [Display(Name = "Suprimento")]
        CashInflows = 1,
        [Comment("Sangria")]
        [Display(Name = "Sangria")]
        Withdrawal = 2,
        [Comment("Despesa")]
        [Display(Name = "Despesas")]
        Expense = 3
    }
}
