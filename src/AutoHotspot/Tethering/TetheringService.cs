namespace AutoHotspot.Tethering;

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Windows.Networking.Connectivity;
using Windows.Networking.NetworkOperators;

internal sealed class TetheringService : ITetheringService
{
    private readonly ILogger<TetheringService> _logger;
    private NetworkOperatorTetheringManager? _tetheringManager;

    public TetheringService(ILogger<TetheringService> logger)
    {
        ArgumentNullException.ThrowIfNull(logger);

        _logger = logger;
    }

    public bool IsEnabled
    {
        get
        {
            var result = TryGetTetheringManager();

            return result.Status is TetheringOperationStatus.Success
                && _tetheringManager!.TetheringOperationalState is TetheringOperationalState.On;
        }
    }

    public async ValueTask<TetheringOperationResult> DisableAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var result = TryGetTetheringManager();

        if (result.Status is not TetheringOperationStatus.Success)
        {
            return result;
        }

        var operationResult = await _tetheringManager!.StopTetheringAsync();
        return new TetheringOperationResult((TetheringOperationStatus)operationResult.Status, operationResult.AdditionalErrorMessage);
    }

    public async ValueTask<TetheringOperationResult> EnableAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var result = TryGetTetheringManager();

        if (result.Status is not TetheringOperationStatus.Success)
        {
            return result;
        }

        var operationResult = await _tetheringManager!.StartTetheringAsync();
        return new TetheringOperationResult((TetheringOperationStatus)operationResult.Status, operationResult.AdditionalErrorMessage);
    }

    private TetheringOperationResult TryGetTetheringManager()
    {
        Debug.Assert(_tetheringManager is null);

        _logger.LogTrace("Creating tethering manager...");

        try
        {
            var internetConnectionProfile = NetworkInformation.GetInternetConnectionProfile();
            _logger.LogTrace("Internet connection profile name: {ProfileName}.", internetConnectionProfile.ProfileName);

            _tetheringManager = NetworkOperatorTetheringManager.CreateFromConnectionProfile(internetConnectionProfile);

            return new TetheringOperationResult(
                Status: TetheringOperationStatus.Success,
                ErrorMessage: null);
        }
        catch (COMException exception)
        {
            _logger.LogError(
                exception, "Failed to initialize tethering manager: {HResult}.",
                exception.HResult.ToString("X8"));

            return new TetheringOperationResult(
                Status: TetheringOperationStatus.Unknown,
                ErrorMessage: "Unable to retrieve tethering manager. Is the WiFi adapter enabled?");
        }
    }
}
