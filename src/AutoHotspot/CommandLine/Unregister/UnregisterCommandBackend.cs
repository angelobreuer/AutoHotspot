namespace AutoHotspot.CommandLine.Unregister;

using System.Threading;
using System.Threading.Tasks;
using AutoHotspot.AutoStart;

internal sealed class UnregisterCommandBackend : ICommandBackend<UnregisterCommandOptions>
{
    private readonly IAutoStartService _autoStartService;

    public UnregisterCommandBackend(IAutoStartService autoStartService)
    {
        ArgumentNullException.ThrowIfNull(autoStartService);

        _autoStartService = autoStartService;
    }

    public async ValueTask<int> RunAsync(UnregisterCommandOptions options, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(options);

        await _autoStartService
            .UnregisterAsync(cancellationToken)
            .ConfigureAwait(false);

        return 0;
    }
}
