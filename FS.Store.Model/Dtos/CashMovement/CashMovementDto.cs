namespace FS.Store.Model.Dtos.CashMovement
{
    public class CashMovementDto
    {
        public decimal Amount { get; set; }

        public string? Description { get; set; }

        public long CashSessionId { get; set; }
    }
}
