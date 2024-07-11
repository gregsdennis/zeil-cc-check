using Microsoft.AspNetCore.Mvc;

namespace ZeilApi.Controllers;

[ApiController]
[Route("credit-card-check")]
public class CreditCardCheckController : ControllerBase
{
	private readonly ILogger<CreditCardCheckController> _logger;

	public CreditCardCheckController(ILogger<CreditCardCheckController> logger)
	{
		_logger = logger;
	}

	[HttpPost(Name = "CheckCreditCardNumber")]
	public IEnumerable<CreditCardCheckResults> Post()
	{
		_logger.LogInformation("Check requested for card number: {}");
		throw new NotImplementedException();
	}
}