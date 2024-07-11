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
        return new CreditCardCheckResponse { IsValid = true };
    }
}