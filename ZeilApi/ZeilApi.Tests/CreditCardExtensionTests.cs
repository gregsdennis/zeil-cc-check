using ZeilApi.Services;

namespace ZeilApi.Tests
{
	public class CreditCardExtensionTests
	{
		[TestCase("1234567890", "12******90")]
		[TestCase("1234567890123456", "1234********3456")]
		[TestCase("12345678901234567890", "12345**********67890")]
		public void MaskCardNumber_Valid(string unmasked, string expected)
		{
			var actual = unmasked.MaskCardNumber();
			
			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test]
		public void MaskCardNumber_ArgumentNull()
		{
			string? unmasked = null;

			Assert.Throws<ArgumentNullException>(() => unmasked!.MaskCardNumber());
		}
	}
}