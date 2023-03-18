namespace AutoHotspot.Tethering;

internal readonly record struct TetheringOperationResult(
    TetheringOperationStatus Status,
    string? ErrorMessage);