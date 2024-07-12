### Would I put this into production as-is?

Absolutely, yes.

This is such simple functionality that it doesn't need the cruft of:

- creating a service interface
- implementing that interface
- injecting the service into the controller
- creating a mock in the API testing layer

I've also another branch (see PR #1), in which I show what I'd _actually_ do, if I were to make this API: just find a well-tested Nuget package that already implements the logic.  There's no need to re-invent the wheel when someone else has already done it (and tested it).

### Why is the verification logic an extension method?

Again, the algorithm is quite simple and can easily and quite reasonably fit into a single function.

It's also a pure function: it always provides the same output for a given input, and it's stateless.

Extension methods can be tested like any other methods.

### Doesn't this approach violate SOLID?

Well, sort of.

If you're trying to apply SOLID to the entire application, then you'll always fail.  Updating the application _at all_ would be a violation of the Open-Closed Principle because you're modifying it.  So, SOLID really only applies to the components of the application and how they interact.

In creating PR #1, I only changed my controller to call the library instead of my extension method.  The other code changes were deletions.  No SOLID violations.