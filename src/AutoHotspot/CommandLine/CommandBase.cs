namespace AutoHotspot.CommandLine;

using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.NamingConventionBinder;
using Microsoft.Extensions.DependencyInjection;

public abstract class CommandBase : Command
{
    private static readonly AsyncLocal<ServiceProvider> _serviceProvider = new();

    protected CommandBase(string name, string? description = null)
        : base(name, description)
    {
    }

    protected static ServiceProvider ServiceProvider
    {
        get
        {
            return _serviceProvider.Value ?? throw new InvalidOperationException("Service provider not assigned.");
        }
    }

    internal static void SetServiceProvider(ServiceProvider serviceProvider)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);

        if (_serviceProvider.Value is not null)
        {
            throw new InvalidOperationException("Service provider already assigned.");
        }

        _serviceProvider.Value = serviceProvider;
    }

}

public abstract class CommandBase<TOptions, TBackend> : CommandBase
    where TOptions : ICommandOptions, new()
    where TBackend : ICommandBackend<TOptions>
{
    protected CommandBase(string name, string? description = null)
        : base(name, description)
    {
        this.SetHandler(ExecuteInternalAsync);
    }

    private async Task ExecuteInternalAsync(InvocationContext invocationContext)
    {
        ArgumentNullException.ThrowIfNull(invocationContext);

        var modelBinder = new ModelBinder<TOptions>();
        var options = ((TOptions?)modelBinder.CreateInstance(invocationContext.BindingContext)) ?? new TOptions();

        var serviceProvider = ServiceProvider;
        await using var _ = serviceProvider.ConfigureAwait(false);

        var backend = serviceProvider.GetRequiredService<ICommandBackend<TOptions>>();
        var cancellationToken = invocationContext.BindingContext.GetRequiredService<CancellationToken>();

        invocationContext.ExitCode = await backend
            .RunAsync(options, cancellationToken)
            .ConfigureAwait(false);
    }
}
