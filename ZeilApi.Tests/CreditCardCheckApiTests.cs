using System.Net;
using System.Text;
using System.Text.Json.Nodes;
using Json.More;
using Microsoft.AspNetCore.Mvc.Testing;
using ZeilApi.CreditCardCheck;

namespace ZeilApi.Tests;

public class CreditCardCheckApiTests
{
	private readonly WebApplicationFactory<Program> _factory = new();

	[TestCase("17893729974")] // from https://en.wikipedia.org/wiki/Luhn_algorithm
	[TestCase("4444444444444448")]
	[TestCase("1234567890123452")]
	public async Task Post_ReturnsValidCardNumber(string cardNumber)
	{
		// Arrange
		var client = _factory.CreateClient();
		var content = new StringContent($$"""
			{
			  "cardNumber": "{{cardNumber}}"
			}                        
			""", Encoding.UTF8, "application/json");

		// Act
		var response = await client.PostAsync("/credit-card-check", content);

		// Assert
		response.EnsureSuccessStatusCode();
		var responseContent = await response.Content.ReadFromJsonAsync<JsonNode>();
		Assert.That(() => responseContent!["isValid"].IsEquivalentTo(true));
	}

	[TestCase("17893729978")]
	[TestCase("4444444444444444")]
	[TestCase("1234567890123456")]
	public async Task Post_ReturnsInvalidCardNumber(string cardNumber)
	{
		// Arrange
		var client = _factory.CreateClient();
		var content = new StringContent($$"""
          {
            "cardNumber": "{{cardNumber}}"
          }
          """, Encoding.UTF8, "application/json");

		// Act
		var response = await client.PostAsync("/credit-card-check", content);

		// Assert
		response.EnsureSuccessStatusCode();
		var responseContent = await response.Content.ReadFromJsonAsync<JsonNode>();
		Assert.That(() => responseContent!["isValid"].IsEquivalentTo(false));
		Assert.That(() => responseContent!["error"].IsEquivalentTo(ErrorMessages.InvalidCheckDigit));
	}

	[TestCase("\"12345\"")] // too short
	[TestCase("\"123456789012345678901\"")] // too long
	[TestCase("\"  17893729978\"")] // leading whitespace
	[TestCase("\"17893729978   \"")] // trailing whitespace
	[TestCase("null")] // null
	[TestCase("\"1234567890abcdef\"")] // non-digits
	public async Task Post_ValidationFailure(string? cardNumber)
	{
		// Arrange
		var client = _factory.CreateClient();
		var content = new StringContent($$"""
          {
            "cardNumber": {{cardNumber}}
          }
          """, Encoding.UTF8, "application/json");

		// Act
		var response = await client.PostAsync("/credit-card-check", content);

		// Assert
		Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
		var responseContent = await response.Content.ReadFromJsonAsync<JsonNode>();
		// this casing is unintuitive, but it seems STJ's casing doesn't apply to dictionary keys
		Assert.That(responseContent!["errors"]!["CardNumber"], Is.Not.Null);
	}
}