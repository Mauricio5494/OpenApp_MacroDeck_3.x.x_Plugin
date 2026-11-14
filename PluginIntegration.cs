using MacroDeck.Sdk;
using MacroDeck.Sdk.Actions;
using Serilog;

namespace OpenAppPlugin;

/// <summary>
/// The plugin's one integration. It declares the OpenApp action.
/// </summary>
public sealed class PluginIntegration : IPluginIntegration
{
    private readonly ILogger _logger;

    public PluginIntegration(ILogger logger)
    {
        _logger = logger.ForContext<PluginIntegration>();
        Actions = [new OpenAppAction(logger)];
    }

    public IReadOnlyList<IActionDefinition> Actions { get; }

    public Task InitializeAsync(IIntegrationContext context)
    {
        _logger.Information("Initialized.");
        return Task.CompletedTask;
    }

    public Task ShutdownAsync() => Task.CompletedTask;
}
