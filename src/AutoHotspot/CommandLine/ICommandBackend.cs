namespace AutoHotspot.CommandLine;

public interface ICommandBackend<TOptions> where TOptions : ICommandOptions
{
    ValueTask<int> RunAsync(TOptions options, CancellationToken cancellationToken = default);
}
