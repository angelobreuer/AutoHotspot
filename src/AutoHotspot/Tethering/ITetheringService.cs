namespace AutoHotspot.Tethering;

using System.Threading.Tasks;

internal interface ITetheringService
{
    bool IsEnabled { get; }

    ValueTask<TetheringOperationResult> EnableAsync(CancellationToken cancellationToken = default);

    ValueTask<TetheringOperationResult> DisableAsync(CancellationToken cancellationToken = default);
}
