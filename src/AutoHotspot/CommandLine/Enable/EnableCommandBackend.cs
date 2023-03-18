namespace AutoHotspot.CommandLine.Enable;

using System.Threading;
using System.Threading.Tasks;
using AutoHotspot.CommandLine.Disable;
using AutoHotspot.Tethering;
using Microsoft.Extensions.Logging;

internal sealed class EnableCommandBackend : ICommandBackend<EnableCommandOptions>
{
    private readonly ITetheringService _tetheringService;
    private readonly ILogger<DisableCommand> _logger;

    public EnableCommandBackend(ITetheringService tetheringService, ILogger<DisableCommand> logger)
    {
        ArgumentNullException.ThrowIfNull(tetheringService);
        ArgumentNullException.ThrowIfNull(logger);

        _tetheringService = tetheringService;
        _logger = logger;
    }

    public async ValueTask<int> RunAsync(EnableCommandOptions options, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(options);

        _logger.LogInformation("Attempting to enable hotspot.");

        var result = await _tetheringService
            .EnableAsync(cancellationToken)
            .ConfigureAwait(false);

        if (result.Status is not TetheringOperationStatus.Success)
        {
            _logger.LogError(
                "Failed to enable hotspot: ({ErrorCode}) {ErrorName}: {ErrorMessage}.",
                (int)result.Status, result.Status.ToString(), result.ErrorMessage ?? "Unknown error");

            return 1;
        }

        _logger.LogInformation("Hotspot enabled.");

        return 0;
    }
}
