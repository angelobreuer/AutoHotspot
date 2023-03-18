namespace AutoHotspot.CommandLine.Status;

internal sealed class StatusCommand : CommandBase<StatusCommandOptions, StatusCommandBackend>
{
    public StatusCommand()
        : base("status", "Retrieves the status of the hotspot (true/false).")
    {
    }
}
