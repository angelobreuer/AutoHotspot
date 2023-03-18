namespace AutoHotspot.CommandLine.Enable;

internal sealed class EnableCommand : CommandBase<EnableCommandOptions, EnableCommandBackend>
{
    public EnableCommand() : base(
        name: "enable",
        description: "Enables the hotspot.")
    {
    }
}
