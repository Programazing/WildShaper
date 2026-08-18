using FastEndpoints;

namespace WildShaper.API.Features.Health.GetLiveness;

public sealed record LivenessResponse(string Status);

public sealed class GetLivenessEndpoint
    : EndpointWithoutRequest<LivenessResponse>
{
    public override void Configure()
    {
        Get("/health/live");
        AllowAnonymous();
        Description(d => d.WithTags("Health"));
    }

    public override Task HandleAsync(CancellationToken ct)
    {
        return Send.OkAsync(
            new LivenessResponse("ok"),
            cancellation: ct);
    }
}