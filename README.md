[![Build & Test](https://github.com/gregsdennis/zeil-cc-check/actions/workflows/dotnet-core.yml/badge.svg?branch=main&event=push)](https://github.com/gregsdennis/zeil-cc-check/actions/workflows/dotnet-core.yml)
[![License](https://img.shields.io/github/license/gregsdennis/zeil-cc-check)](https://github.com/gregsdennis/zeil-cc-check/blob/main/LICENSE)

# ziel-cc-check

_Public for a limited time only!_

Welcome to the ZEIL Check Digit Verifier API!

This API serves just one purpose:  verifying the check digit on a string of digits using the [Luhn algorithm](https://en.wikipedia.org/wiki/Luhn_algorithm) commonly used on credit cards.

_**IMPORTANT** The check digit is not a cryptographic measure.  It only serves as a verification against things like typing mistakes._

## Building and Running the Project

The project is a .Net 8 ASP.Net Web API.  To build it, you'll need to install the [.Net 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0).

It can be built and run in your favorite .Net IDE, such as Visual Studio, Visual Studio Code (with C# extensions), or Jetbrains Rider.

If you prefer to use the command line, use

```
dotnet run
```

This will build and run the API in debug mode.  See the [`dotnet` CLI reference](https://learn.microsoft.com/en-us/dotnet/core/tools/) for more information.

## Calling the API

The API exposes a single endpoint: `POST /card-number-check`.  The content of the request is JSON that meets the following [JSON Schema](https://json-schema.org/):

```json
{
  "$schema": "https://json-schema.org/draft/2020-12/schema",
  "type": "object",
  "properties": {
    "cardNumber": {
      "type": "string",
      "pattern": "^[0-9]{10,20}$"
    }
  },
  "required": ["cardNumber"],
  "additionalProperties": false
}
```

This is a simple object with a single property `cardNumber`.  The value for `cardNumber` must be a string of between 10 and 20 (inclusive) digits.

Given a valid request, the response will contain a JSON object with two properties:

- `value` - a boolean indicating whether the check digit is correct.
- `error` - null if the check digit is correct; a string with an error message if the check digit is incorrect.