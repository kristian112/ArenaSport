using System.ComponentModel.DataAnnotations;

namespace ArenaSportProj.Models.ViewModels;

public class CheckoutViewModel
{
    [Required(ErrorMessage = "Адресът е задължителен.")]
    public string Address { get; set; } = null!;

    [Required(ErrorMessage = "Телефонът е задължителен.")]
    public string PhoneNumber { get; set; } = null!;

    public string PaymentMethod { get; set; } = "Наложен платеж";
}
