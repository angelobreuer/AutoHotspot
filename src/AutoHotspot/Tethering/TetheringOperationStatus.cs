namespace AutoHotspot.Tethering;

internal enum TetheringOperationStatus : byte
{
    Success,
    Unknown,
    MobileBroadbandDeviceOff,
    WiFiDeviceOff,
    EntitlementCheckTimeout,
    EntitlementCheckFailure,
    OperationInProgress,
    BluetoothDeviceOff,
    NetworkLimitedConnectivity
}
