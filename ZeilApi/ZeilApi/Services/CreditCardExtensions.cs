namespace ZeilApi.Services;

public static class CreditCardExtensions
{
	/// <summary>
	/// Verifies Luhn algorithm check digit on credit card numbers.  Numbers must be 16 digits, no dashes or spaces.
	/// </summary>
	/// <remarks>See https://en.wikipedia.org/wiki/Luhn_algorithm.</remarks>>
	public static bool VerifyCheckDigit(this string cardNumber)
	{
		var payload = new int[cardNumber.Length - 1];

		// convert to numbers
		for (int i = 0; i < cardNumber.Length - 1; i++)
		{
			payload[i] = cardNumber[i] - '0';
		}

		// apply multiplier
		for (int i = 0; i < cardNumber.Length - 1; i++)
		{
			var multiplier = 2 - i % 2;
			payload[^(i + 1)] *= multiplier;
		}
		
		// individual digit sums
		for (int i = 0; i < cardNumber.Length - 1; i++)
		{
			var value = payload[i];
			var sum = 0;
			while (value != 0)
			{
				sum += value % 10;
				value /= 10;
			}
			payload[i] = sum;
		}
		
		// sum all digits
		var total = payload.Sum();
		var expectedCheckDigit = 10 - total % 10;

		var actual = cardNumber[^1] - '0';

		return expectedCheckDigit == actual;
	}
}