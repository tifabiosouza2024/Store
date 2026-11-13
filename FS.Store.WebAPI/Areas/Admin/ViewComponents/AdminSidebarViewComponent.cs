using FS.Store.Core.Helper;
using FS.Store.Core.Interfaces;
using FS.Store.WebAPI.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FS.Store.WebAPI.Areas.Admin.ViewComponents
{
    public class AdminSidebarViewComponent(ICashRegisterSessionService cashRegisterSessionService
        , CurrentUserHelper currentUserHelper) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cashSessionId = GetCurrentCashSessionId();

            if (cashSessionId == null)
            {
                var sessions = await cashRegisterSessionService.GetCashRegisterSessionOpenedAsync();

                if (sessions.Count() == 1)
                    cashSessionId = sessions.First().Id;
                else
                    cashSessionId = sessions.FirstOrDefault(c => c.OpenedByUserId.Equals(currentUserHelper.UserId.GetValueOrDefault()))?.Id;
            }
            if (cashSessionId != null)
                HttpContext.Session.SetString("CurrentCashSessionId", cashSessionId.Value.ToString());

            var model = new AdminSidebarViewModel
            {
                CashSessionId = cashSessionId
            };

            return View(model);
        }

        private long? GetCurrentCashSessionId()
        {
            var value = RouteData.Values["cashSessionId"] ?? HttpContext.Request.Query["cashSessionId"].FirstOrDefault();

            if (long.TryParse(value?.ToString(), out var id))
                return id;

            var sessionValue = HttpContext.Session.GetString("CurrentCashSessionId");

            return long.TryParse(sessionValue, out id) ? id : null;
        }
    }
}
