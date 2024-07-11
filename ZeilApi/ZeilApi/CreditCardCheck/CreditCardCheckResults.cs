namespace ZeilApi.CreditCardCheck;

public class CreditCardCheckResponse
{
    public bool IsValid { get; set; }
    public string? Error { get; set; }
}