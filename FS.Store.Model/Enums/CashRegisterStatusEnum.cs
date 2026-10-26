using Microsoft.EntityFrameworkCore;

namespace FS.Store.Model.Enums
{
    /// <summary>
    /// Status de caixa, sendo aberto, fechado ou cancelado
    /// </summary>
    public enum CashRegisterStatusEnum
    {
        [Comment("Aberto")]
        Open = 0,
        [Comment("Fechado")]
        Close = 1,
        [Comment("Cancelado")]
        Canceled = 2
    }
}
