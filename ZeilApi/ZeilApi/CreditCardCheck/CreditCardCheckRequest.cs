using System.ComponentModel.DataAnnotations;

namespace ZeilApi.CreditCardCheck;

public class CreditCardCheckRequest
{
    [Required]
    [RegularExpression("^[0-9]{10,20}$")]
    public string CardNumber { get; set; }
}