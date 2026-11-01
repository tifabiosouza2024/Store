using FS.Store.Core.Extension;
using FS.Store.Core.Interfaces;
using FS.Store.WebAPI.Areas.Admin.ViewModels;
using FS.Store.WebAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FS.Store.WebAPI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SUPER,ADMIN,MANAGER")]
    public class PaymentTerminalController(IPaymentTerminalService paymentTerminalService
        , IPaymentProviderService paymentProvider) : Controller
    {
        public async Task<IActionResult> Index()
        {
            PaymentTerminalIndexViewModel vm = new();
            var terminals = await paymentTerminalService.GetAllAsync();

            vm.PaymentTerminals = terminals;
            return View(vm);
        }

        public async Task<IActionResult> Create()
        {
            PaymentTerminalCreateViewModel vm = new();
            var providers = await paymentProvider.GetAllAsync();
            if (providers == null || !providers.Any())
            {
                ModelState.AddModelError(string.Empty, "Não existem fornecedores cadastrados.");
                return this.RedirectAdminToBadRequest("Não existem fornecedores cadastrados. Por favor, entre em contato com o administrador do sistema.");
            }

            vm.PaymentProviders = [.. providers.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name })];
            vm.OperationName = User.GetOperationName();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PaymentTerminalCreateViewModel terminal)
        {
            if (!ModelState.IsValid)
            {
                var providers = await paymentProvider.GetAllAsync();

                terminal.PaymentProviders = [.. providers!.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name })];
                terminal.OperationName = User.GetOperationName();
                return View(terminal);
            }


            await paymentTerminalService.AddOrUpdateAsync(new() { Name = terminal.Name, PaymentProviderId = terminal.PaymentProviderId });

            return RedirectToAction("Index");
        }
    }
}
