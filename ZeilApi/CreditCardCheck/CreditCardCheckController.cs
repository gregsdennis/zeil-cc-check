using Microsoft.AspNetCore.Mvc;
using ZeilApi.Infrastructure;
using ZeilApi.Services;

namespace ZeilApi.CreditCardCheck;

[ApiController]
[Route("card-number-check")]
public class CreditCardCheckController : ControllerBase
{
    [ValidateModel]
    [HttpPost(Name = "CardNumberCheck")]
    public CreditCardCheckResponse Post(CreditCardCheckRequest request)
    {
	    var checkDigitIsValid = request.CardNumber.VerifyCheckDigit();

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