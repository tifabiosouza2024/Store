using FS.Store.Core.Helper;
using FS.Store.Core.Interfaces;
using FS.Store.WebAPI.Services.Interface;

namespace FS.Store.WebAPI.Services
{
    public class CurrentCashSessionService(
            IHttpContextAccessor httpContextAccessor,
            ICashRegisterSessionService cashRegisterSessionService,
            CurrentUserHelper currentUserHelper) : ICurrentCashSessionService
    {
        private const string SessionKey = "CurrentCashSessionId";


        public long? Id
        {
            get
            {
                var value = httpContextAccessor.HttpContext?.Session.GetString(SessionKey);

                return long.TryParse(value, out var id) ? id : null;
            }
        }

        public async Task<long?> GetOrResolveAsync()
        {
            // Primeiro verifica se já existe um contexto armazenado.
            if (Id.HasValue)
                return Id;

            // Se não existir, consulta as sessões abertas.
            var sessions = (await cashRegisterSessionService.GetCashRegisterSessionOpenedAsync()).ToList();

            long? cashSessionId;

            if (sessions.Count == 1)
            {
                cashSessionId = sessions[0].Id;
            }
            else
            {
                cashSessionId = sessions.FirstOrDefault(c => c.OpenedByUserId == currentUserHelper.UserId)?.Id;
            }

            // Armazena o contexto encontrado.
            if (cashSessionId.HasValue)
                Set(cashSessionId.Value);

            return cashSessionId;
        }

        public void Set(long id)
        {
            httpContextAccessor.HttpContext?.Session.SetString(SessionKey, id.ToString());
        }

        public void Clear()
        {
            httpContextAccessor.HttpContext?.Session.Remove(SessionKey);
        }
    }
}
