namespace AutoHotspot.AutoStart;

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32;

internal sealed class AutoStartService : IAutoStartService
{
    private const string ApplicationName = "AutoHotspot.90a42e3c34a941158c26fb018186c778";

    public ValueTask RegisterAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        using var registryKey = Registry.CurrentUser.OpenSubKey(
            name: "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run",
            writable: true);

        registryKey!.SetValue(ApplicationName, $"\"{Environment.ProcessPath}\" enable");

        return default;
    }

    public ValueTask UnregisterAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        using var registryKey = Registry.CurrentUser.OpenSubKey(
            name: "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run",
            writable: true);

        registryKey!.DeleteValue(ApplicationName);

        return default;
    }
}
