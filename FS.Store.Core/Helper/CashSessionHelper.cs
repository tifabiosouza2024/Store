using FS.Store.Core.Exceptions;
using FS.Store.Core.Interfaces;
using FS.Store.Model.Dtos.CashClosing;
using FS.Store.Model.Dtos.CashMovement;
using FS.Store.Model.Entity;
using FS.Store.Model.Enums;

namespace FS.Store.Core.Helper
{
    public class CashSessionHelper(ICashClosingService cashClosingService
        , ICashRegisterSessionService cashRegisterSessionService
        , ICashMovementService cashMovementService
        , CurrentUserHelper currentUserHelper)
    {
        public async Task CloseSessionAsync(CashClosingDto sessionVM)
        {
            var userId = currentUserHelper.UserId.GetValueOrDefault();
            var now = DateTime.Now;
            var closing = new CashClosing();
            var session = await cashRegisterSessionService.GetToClose(sessionVM.CashRegisterSessionId);
            ServiceException.ThrowIfNull(session, "Sessão de caixa não localizada para fechamento");

            closing.CashAmount = sessionVM.CashAmount;
            closing.ClosedAt = now;
            closing.CashRegisterSessionId = sessionVM.CashRegisterSessionId;
            closing.ClosedByUserId = userId;
            closing.TotalCredit = sessionVM.Terminals.Sum(t => t.CreditAmount);
            closing.TotalDebit = sessionVM.Terminals.Sum(t => t.DebitAmount);
            closing.TotalPix = sessionVM.Terminals.Sum(t => t.PixAmount);
            closing.Notes = sessionVM.Notes;
            await cashClosingService.AddOrUpdateAsync(closing);

            foreach (var terminal in sessionVM.Terminals)
            {
                if (terminal.CreditAmount == 0 && terminal.DebitAmount == 0 && terminal.PixAmount == 0)
                    continue;

                closing.Terminals!.Add(new()
                {
                    CashRegisterSessionId = sessionVM.CashRegisterSessionId,
                    CreatedByUserId = userId,
                    CreditAmount = terminal.CreditAmount,
                    DebitAmount = terminal.DebitAmount,
                    PixAmount = terminal.PixAmount,
                    PaymentTerminalId = terminal.PaymentTerminalId
                });
            }
            session!.ClosedByUserId = userId;
            session.CashRegisterStatus = CashRegisterStatusEnum.Closed;
            session.ClosedAt = now;
            await cashClosingService.SaveChangesAsync();
        }

        public async Task RegisterCashInflows(long cashSessionId, CashMovementDto dto, CashMovementTypeEnum type)
        {
            var userId = currentUserHelper.UserId.GetValueOrDefault();
            var now = DateTime.Now;
            var session = await cashRegisterSessionService.GetToClose(cashSessionId);

            ServiceException.ThrowIfNull(session, "Sessão de caixa não localizada para registro de entrada de dinheiro");

            var cashMovement = new CashMovement
            {
                Amount = dto.Amount,
                Description = dto.Description,
                CashRegisterSessionId = cashSessionId,
                CreatedByUserId = userId,
                OccurredAt = now,
                CashMovementType = type

            };

            await cashMovementService.AddOrUpdateAsync(cashMovement);
        }
    }
}
