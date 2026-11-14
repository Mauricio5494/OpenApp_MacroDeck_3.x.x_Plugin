# Open App Plugin for Macro Deck 3

A Macro Deck 3 plugin that opens a local application or file from a button. You select the executable path (or any file) and Macro Deck launches it using the default application handler.

**Important:** This plugin is for **Macro Deck 3 only**. It is **not** compatible with Macro Deck 2.x.

## What it does

With one button press, this plugin opens a single application or file:

- **.exe files** — launched directly on Windows
- **.txt, .pdf, and other files** — opened with the default associated application
- Configure the executable path per button instance
- Cross-platform: works on Windows, Linux, and macOS

## Features

- Open any local application by its executable path
- Open documents with the default associated application
- Simple text parameter for the executable/file path
- Cross-platform entrypoints: Windows (win-x64), Linux (linux-x64), macOS (osx-arm64)
- Framework-dependent: small artifact size (~0.7 MB per platform), Macro Deck 3 provides the .NET 10 runtime

## Requirements

- **Macro Deck 3.0.0 or later**
- Windows 10/11, Linux, or macOS

## Installation

The plugin is distributed as a **`.macroDeckPlugin`** package file:

```
com.macrodeck.openapp-1.0.0.macroDeckPlugin
```

This file is located in the `artifacts/` folder of the project.

### Installing in Macro Deck 3

Macro Deck 3 includes a built-in **Plugins** section where third-party plugins can be added. To install this plugin:

1. Open Macro Deck 3.
2. Go to the **Plugins** section (or Extensions, depending on your build).
3. Look for an option to **install** or **add** a plugin from a local file (for example, a button labeled "Install plugin", "Add plugin", or similar).
4. Select the `.macroDeckPlugin` file:
   ```
   com.macrodeck.openapp-1.0.0.macroDeckPlugin
   ```
5. Follow the on-screen prompts to complete the installation.
6. Restart Macro Deck 3 if prompted.

> **Note:** The official Macro Deck documentation does not currently publish a step-by-step UI guide for installing local `.macroDeckPlugin` files. If the option is not visible in your build, consult the in-app help or the Macro Deck community resources for the exact menu location in your version.

### What is inside the `.macroDeckPlugin` file?

It is a ZIP archive containing everything the plugin needs:

```
com.macrodeck.openapp-1.0.0.macroDeckPlugin
├── manifest.json
├── runtimes/
│   ├── win-x64/
│   │   ├── OpenAppPlugin.dll
│   │   ├── OpenAppPlugin.deps.json
│   │   └── OpenAppPlugin.runtimeconfig.json
│   ├── linux-x64/
│   │   └── OpenAppPlugin.dll
│   └── osx-arm64/
│       └── OpenAppPlugin.dll
└── Assets/
    └── icon.svg
```

Because it is **framework-dependent**, Macro Deck 3 supplies the .NET 10 runtime, so the artifact only contains the plugin's own assemblies — no large runtime bundles.

## Usage

1. Enter **Edit Mode** in Macro Deck 3.
2. Add a new button or edit an existing one.
3. In the action selector, choose **"Open App"** (Abrir Aplicación).
4. Configure the action:
   - **Executable path**: Enter the full path to the `.exe` or file you want to open (e.g. `C:\Windows\notepad.exe` on Windows, `/usr/bin/firefox` on Linux, `/Applications/Safari.app` on macOS).
5. Save the button and exit Edit Mode.
6. Press the button — the application or file will open.

## Development

The plugin source code is located at:

```
/home/sparky/Documentos/Macro Deck proyect scripts/Macro Deck proyect scritps/OpenAppPlugin/
```

### Project structure

| File | Purpose |
|------|---------|
| `manifest.json` | Plugin identity, version, entrypoints per platform |
| `Program.cs` | Entry point; registers the integration and starts the plugin host |
| `PluginIntegration.cs` | Declares the `Open App` action |
| `OpenAppAction.cs` | Action definition and executor (opens the executable path) |
| `Localization/Strings.resx` | Localized strings (Spanish by default) |
| `Assets/icon.svg` | Plugin icon |
| `macrodeck-build.json` | Build instructions per platform |
| `OpenAppPlugin.csproj` | .NET 10 project, framework-dependent, ASP.NET Core |

### Building

```bash
dotnet tool install --global MacroDeck.Plugin.Cli --prerelease

macrodeck-plugin build --output ../../artifacts
```

This produces `com.macrodeck.openapp-1.0.0.macroDeckPlugin` in the `artifacts` folder.

### Validating

```bash
macrodeck-plugin validate --artifact artifacts/com.macrodeck.openapp-1.0.0.macroDeckPlugin
```

## Compatibility

| Platform | Supported |
|----------|-----------|
| Windows | Yes |
| Linux | Yes |
| macOS | Yes |
| Macro Deck 3 | Yes |
| Macro Deck 2.x | No |

## License

MIT License

## Sources

- [Macro Deck Docs](https://docs.macro-deck.app/)
- [Macro Deck User Guide](https://docs.macro-deck.app/guide/)
- [Macro Deck Plugin CLI](https://docs.macro-deck.app/cli/)
- [Macro Deck Quickstart](https://docs.macro-deck.app/introduction/quickstart/)