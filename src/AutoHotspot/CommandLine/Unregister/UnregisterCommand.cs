namespace AutoHotspot.CommandLine.Unregister;
internal sealed class UnregisterCommand : CommandBase<UnregisterCommandOptions, UnregisterCommandBackend>
{
    public UnregisterCommand() : base(
        name: "unregister",
        description: "Unregisters the auto start key to automatically enable the hotspot.")
    {
    }
}
