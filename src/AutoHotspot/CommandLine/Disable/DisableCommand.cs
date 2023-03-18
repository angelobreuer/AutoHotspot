namespace AutoHotspot.CommandLine.Disable;

internal sealed class DisableCommand : CommandBase<DisableCommandOptions, DisableCommandBackend>
{
    public DisableCommand() : base(
        name: "disable",
        description: "Disables the hotspot.")
    {
    }
}
