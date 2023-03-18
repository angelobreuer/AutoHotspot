namespace AutoHotspot.CommandLine.Register;

internal sealed class RegisterCommand : CommandBase<RegisterCommandOptions, RegisterCommandBackend>
{
    public RegisterCommand() : base(
        name: "register",
        description: "Registers the auto start key to automatically enable the hotspot.")
    {
    }
}
