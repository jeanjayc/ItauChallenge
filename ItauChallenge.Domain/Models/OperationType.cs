using System.ComponentModel.DataAnnotations;

namespace ItauChallenge.Domain.Models
{
    public enum OperationType
    {
        [Display(Name = "Compra")]
        Buy = 1,

        [Display(Name = "Venda")]
        Sell = 2
    }
}
