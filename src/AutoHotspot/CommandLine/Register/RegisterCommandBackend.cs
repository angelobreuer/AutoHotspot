namespace AutoHotspot.CommandLine.Register;

using System.Threading;
using System.Threading.Tasks;
using AutoHotspot.AutoStart;

internal sealed class RegisterCommandBackend : ICommandBackend<RegisterCommandOptions>
{
    private readonly IAutoStartService _autoStartService;

    public RegisterCommandBackend(IAutoStartService autoStartService)
    {
        ArgumentNullException.ThrowIfNull(autoStartService);

        _autoStartService = autoStartService;
    }

    public async ValueTask<int> RunAsync(RegisterCommandOptions options, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(options);

        await _autoStartService
            .RegisterAsync(cancellationToken)
            .ConfigureAwait(false);

        return 0;
    }
}
