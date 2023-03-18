using System.CommandLine;
using AutoHotspot.AutoStart;
using AutoHotspot.CommandLine;
using AutoHotspot.CommandLine.Disable;
using AutoHotspot.CommandLine.Enable;
using AutoHotspot.CommandLine.Register;
using AutoHotspot.CommandLine.Status;
using AutoHotspot.CommandLine.Unregister;
using AutoHotspot.Tethering;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var services = new ServiceCollection();

var verboseEnvironmentVariableValue = Environment.GetEnvironmentVariable("AUTOHOTSPOT_VERBOSE");
var verbose = verboseEnvironmentVariableValue is not null && verboseEnvironmentVariableValue.Equals("1");
var logLevel = verbose ? LogLevel.Trace : LogLevel.Warning;

services.AddTransient<ICommandBackend<StatusCommandOptions>, StatusCommandBackend>();
services.AddTransient<ICommandBackend<EnableCommandOptions>, EnableCommandBackend>();
services.AddTransient<ICommandBackend<DisableCommandOptions>, DisableCommandBackend>();
services.AddTransient<ICommandBackend<RegisterCommandOptions>, RegisterCommandBackend>();
services.AddTransient<ICommandBackend<UnregisterCommandOptions>, UnregisterCommandBackend>();

services.AddSingleton<ITetheringService, TetheringService>();
services.AddSingleton<IAutoStartService, AutoStartService>();

services.AddLogging(x => x.AddConsole().SetMinimumLevel(logLevel));

using var serviceProvider = services.BuildServiceProvider();

CommandBase.SetServiceProvider(serviceProvider);
return await new CliRootCommand().InvokeAsync(args).ConfigureAwait(false);