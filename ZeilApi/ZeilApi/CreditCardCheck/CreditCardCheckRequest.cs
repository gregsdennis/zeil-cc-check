using System.ComponentModel.DataAnnotations;

namespace ZeilApi.CreditCardCheck;

public class CreditCardCheckRequest
{
    [Required]
    [RegularExpression("^[0-9]{16}$")]
    public string CardNumber { get; set; }
}