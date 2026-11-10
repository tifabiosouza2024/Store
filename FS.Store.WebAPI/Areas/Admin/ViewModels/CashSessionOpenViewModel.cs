using System.ComponentModel.DataAnnotations;

namespace FS.Store.WebAPI.Areas.Admin.ViewModels
{
    public class CashSessionOpenViewModel
    {
        public string? OperationName { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Para o valor de abertura, é necessario um valor maior que zero.")]
        public decimal Amount { get; set; }

        [Range(1, 100, ErrorMessage = "O Número do caixa precisa estar entre 1 e 100")]
        public int CashSessionNumber { get; set; }
        public bool CashSessionAlreadyExists { get; set; }
    }
}
