using FS.Store.Core.Exceptions;
using FS.Store.Core.Interfaces;
using FS.Store.Model.Dtos;
using FS.Store.Model.Entity;
using FS.Store.Model.Enums;

namespace FS.Store.Core.Helper
{
    public class CashSessionHelper(ICashClosingService cashClosingService
        , ICashRegisterSessionService cashRegisterSessionService
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
            session.CashRegisterStatus = CashRegisterStatusEnum.Close;
            session.ClosedAt = now;
            await cashClosingService.SaveChangesAsync();
        }
    }
}
