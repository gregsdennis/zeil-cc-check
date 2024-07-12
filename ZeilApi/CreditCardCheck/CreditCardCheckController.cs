using LuhnNet;
using Microsoft.AspNetCore.Mvc;
using ZeilApi.Infrastructure;

namespace ZeilApi.CreditCardCheck;

[ApiController]
[Route("credit-card-check")]
public class CreditCardCheckController : ControllerBase
{
    [ValidateModel]
    [HttpPost(Name = "CheckCreditCardNumber")]
    public CreditCardCheckResponse Post(CreditCardCheckRequest request)
    {
	    var checkDigitIsValid = Luhn.IsValid(request.CardNumber);

        return new CreditCardCheckResponse
        {
	        IsValid = checkDigitIsValid,
			Error = checkDigitIsValid ? null : ErrorMessages.InvalidCheckDigit
        };
    }
}

public static class ErrorMessages
{
	public const string InvalidCheckDigit = "The check digit does not match expected value";
}