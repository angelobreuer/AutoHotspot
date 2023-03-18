namespace AutoHotspot.AutoStart;

using System.Threading.Tasks;

internal interface IAutoStartService
{
    ValueTask RegisterAsync(CancellationToken cancellationToken = default);

    ValueTask UnregisterAsync(CancellationToken cancellationToken = default);
}
