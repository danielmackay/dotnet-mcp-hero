# Use Minimal API Typed Results

- Status: accepted
- Deciders: Daniel Mackay, William Liebenberg
- Date: 2024-06-05
- Tags: minimal-api, typed-results

Technical Story: https://github.com/SSWConsulting/HeroApi/issues/137

## Context and Problem Statement

Currently, our APIs use the `Result` pattern to return a `Result<T>` object. This works OK, but additional metadata is required to determine the status code of the response. We need to add the metadata manually which means there is a chance of inconsistency between the response and the status code.

We have tried to get around this by using helper methods to create the consistent OpenAI metadata for each REST verb.  This was an improvement, but we still have the problem of the API status codes not being consistent with the response.

For example:

```csharp
/// <summary>
/// Used for POST endpoints that creates a single item.
/// </summary>
public static RouteHandlerBuilder ProducesPost(this RouteHandlerBuilder builder) => builder
    .Produces(StatusCodes.Status201Created)
    .ProducesValidationProblem()
    .ProducesProblem(StatusCodes.Status500InternalServerError);
```

The problem here is that we advertise returning a 201, but the API might return a 200.

This inconsistency, makes integrating with our APIs difficult.

## Considered Options

1. Use Results (current implementation)
2. Use TypedResults

## Decision Outcome

Chosen option: "Option 2 - Use TypedResults", because it allows us to guarantee our APIs return what they say they do.

### Consequences <!-- optional -->

- Dependency on additional nuget packages

## Pros and Cons of the Options <!-- optional -->

### Option 1 - Use Results (current implementation)

As per the current implementation.

- ✅ More concise code
- ❌ Allows for inconsistencies between the response and the status code

For example:

```csharp
group
    .MapPost("/{teamId:guid}/heroes/{heroId:guid}",
        async (ISender sender, Guid teamId, Guid heroId, CancellationToken ct) =>
        {
            var command = new AddHeroToTeamCommand(teamId, heroId);
            await sender.Send(command, ct);
            return Results.Ok();
        })
    .WithName("AddHeroToTeam")
    .ProducesPost();
```

### Option 2 - Use TypedResults

- ✅ Guarantees the response status code matches the response
- ❌ More verbose code

For example:

```csharp
group
    .MapPost("/{teamId:guid}/heroes/{heroId:guid}",
        async Task<Results<ValidationProblem, NotFound<string>, Created>> (ISender sender, Guid teamId, Guid heroId, CancellationToken ct) =>
        {
            var command = new AddHeroToTeamCommand(teamId, heroId);
            var result = await sender.Send(command, ct);

            if (result.IsInvalid())
                return TypedResultsExt.ValidationProblem(result);

            if (result.IsNotFound())
                return TypedResultsExt.NotFound(result);

            return TypedResults.Created();
        })
    .WithName("AddHeroToTeam")
    .ProducesProblem(StatusCodes.Status500InternalServerError);
```

## Links <!-- optional -->

- https://www.dandoescode.com/blog/minimal-apis-typed-results-and-open-api
