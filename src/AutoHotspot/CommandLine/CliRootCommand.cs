namespace AutoHotspot.CommandLine;

using AutoHotspot.CommandLine.Disable;
using AutoHotspot.CommandLine.Enable;
using AutoHotspot.CommandLine.Register;
using AutoHotspot.CommandLine.Status;
using AutoHotspot.CommandLine.Unregister;

internal sealed class CliRootCommand : CommandBase
{
    public CliRootCommand() : base(
        name: Path.GetFileNameWithoutExtension(Environment.ProcessPath)!.Replace(" ", ""),
        description: string.Empty)
    {
        AddCommand(new StatusCommand());
        AddCommand(new EnableCommand());
        AddCommand(new DisableCommand());
        AddCommand(new RegisterCommand());
        AddCommand(new UnregisterCommand());
    }
}
