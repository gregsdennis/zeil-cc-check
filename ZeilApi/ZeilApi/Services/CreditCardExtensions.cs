using System.Text;

namespace ZeilApi.Services;

public static class CreditCardExtensions
{
	/// <summary>
	/// Masks the middle section of a card number.
	/// </summary>
	public static string MaskCardNumber(this string rawCardNumber)
	{
		ArgumentNullException.ThrowIfNull(rawCardNumber);
		ArgumentException.ThrowIfNullOrWhiteSpace(rawCardNumber);
		if (rawCardNumber.Any(x => !char.IsAsciiDigit(x)))
			throw new ArgumentException($"Argument '{nameof(rawCardNumber)}' must only contain digits");
		if (rawCardNumber.Length < 4)
			throw new ArgumentException($"Argument '{nameof(rawCardNumber)}' must contain at least 4 digits");

		var unmaskedLength = rawCardNumber.Length / 4;
		var builder = new StringBuilder();
		builder.Append(rawCardNumber[..unmaskedLength]);
		var maskedChars = rawCardNumber.Length - unmaskedLength * 2;
		for (int i = 0; i < maskedChars; i++)
		{
			builder.Append('*');
		}

		builder.Append(rawCardNumber[^unmaskedLength..]);

		return builder.ToString();
	}
}