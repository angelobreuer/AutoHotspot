namespace AutoHotspot.CommandLine.Status;

using System.Threading;
using System.Threading.Tasks;
using AutoHotspot.Tethering;

internal sealed class StatusCommandBackend : ICommandBackend<StatusCommandOptions>
{
    private readonly ITetheringService _tetheringService;

    public StatusCommandBackend(ITetheringService tetheringService)
    {
        ArgumentNullException.ThrowIfNull(tetheringService);

        _tetheringService = tetheringService;
    }

    public ValueTask<int> RunAsync(StatusCommandOptions options, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(options);

        var enabled = _tetheringService.IsEnabled;
        Console.WriteLine(enabled ? "true" : "false");

        return default;
    }
}
