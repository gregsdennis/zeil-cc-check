using ZeilApi.Services;

namespace ZeilApi.Tests;

public class CreditCardExtensionTests
{
	[TestCase("17893729974")] // from https://en.wikipedia.org/wiki/Luhn_algorithm
	[TestCase("4444444444444448")]
	[TestCase("1234567890123452")]
	public void VerifyCheckDigit_Pass(string cardNumber)
	{
		Assert.That(cardNumber.VerifyCheckDigit, Is.True);
	}

	[TestCase("17893729978")]
	[TestCase("4444444444444444")]
	[TestCase("1234567890123456")]
	public void VerifyCheckDigit_Fail(string cardNumber)
	{
		Assert.That(cardNumber.VerifyCheckDigit, Is.False);
	}
}