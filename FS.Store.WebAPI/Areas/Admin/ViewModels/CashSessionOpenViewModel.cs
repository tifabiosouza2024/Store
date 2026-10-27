using System.ComponentModel.DataAnnotations;

namespace FS.Store.WebAPI.Areas.Admin.ViewModels
{
    public class CashSessionOpenViewModel
    {
        public string? OperationName { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "é necessario um valor positivo para abertura de caixa.")]
        public decimal Amount { get; set; }

        public bool CashSessionAlreadyExists { get; set; }
    }
}
