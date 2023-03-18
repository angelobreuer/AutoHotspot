namespace AutoHotspot.CommandLine.Disable;

using System.Threading;
using System.Threading.Tasks;
using AutoHotspot.Tethering;
using Microsoft.Extensions.Logging;

internal sealed class DisableCommandBackend : ICommandBackend<DisableCommandOptions>
{
    private readonly ITetheringService _tetheringService;
    private readonly ILogger<DisableCommand> _logger;

    public DisableCommandBackend(ITetheringService tetheringService, ILogger<DisableCommand> logger)
    {
        ArgumentNullException.ThrowIfNull(tetheringService);
        ArgumentNullException.ThrowIfNull(logger);

        _tetheringService = tetheringService;
        _logger = logger;
    }

    public async ValueTask<int> RunAsync(DisableCommandOptions options, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(options);

        _logger.LogInformation("Attempting to disable hotspot.");

        var result = await _tetheringService
            .DisableAsync(cancellationToken)
            .ConfigureAwait(false);

        if (result.Status is not TetheringOperationStatus.Success)
        {
            _logger.LogError(
                "Failed to disable hotspot: ({ErrorCode}) {ErrorName}: {ErrorMessage}.",
                (int)result.Status, result.Status.ToString(), result.ErrorMessage ?? "Unknown error");

            return 1;
        }

        _logger.LogInformation("Hotspot disabled.");

        return 0;
    }
}
