using System.Globalization;
using MacroDeck.Localization;
using MacroDeck.Sdk;
using MacroDeck.Sdk.Actions;
using Serilog;

namespace OpenAppPlugin;

/// <summary>
/// Action that opens a local application by its executable path.
/// </summary>
public sealed class OpenAppAction(ILogger logger) : IActionDefinition
{
    private const string AppPathParameter = "appPath";

    private readonly ILogger _logger = logger.ForContext<OpenAppAction>();

    public string Id => "open-app";

    public LocalizedText Name => Strings.Actions.OpenApp.Name();

    public LocalizedText Description => Strings.Actions.OpenApp.Description();

    public MacroDeckPlatform Platforms => MacroDeckPlatform.All;

    public IReadOnlyList<ActionParameter> Parameters { get; } =
    [
        ActionParameter.Text(
            AppPathParameter,
            label: Strings.Actions.OpenApp.AppPathLabel(),
            required: true
        ),
    ];

    public IActionExecutor CreateExecutor() => new Executor(_logger);

    private sealed class Executor : IActionExecutor
    {
        private readonly ILogger _logger;

        public Executor(ILogger logger) => _logger = logger;

        public Task<ActionResult> ExecuteAsync(ActionExecutionContext context)
        {
            var appPath = context.Parameters.TryGetValue(AppPathParameter, out var value)
                ? value?.ToString()
                : null;

            if (string.IsNullOrWhiteSpace(appPath))
            {
                return Task.FromResult(ActionResult.Failed(
                    ActionErrorCodes.InvalidParameter,
                    Strings.Errors.AppPathRequired()));
            }

            try
            {
                var startInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = appPath,
                    UseShellExecute = true,
                };

                System.Diagnostics.Process.Start(startInfo);

                _logger.Information("Aplicación abierta: {AppPath}", appPath);
                return ActionResult.SucceededTask;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "No se pudo abrir la aplicación: {AppPath}", appPath);
                return Task.FromResult(ActionResult.Failed(
                    ActionErrorCodes.NotFound,
                    Strings.Errors.CouldNotOpenApp(appPath: appPath)));
            }
        }
    }
}
