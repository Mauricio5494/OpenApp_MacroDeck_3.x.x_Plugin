# Open App Plugin for Macro Deck 3

Un plugin de Macro Deck 3 que abre una aplicación o archivo local desde un botón. Eliges la ruta del ejecutable (o cualquier archivo) y Macro Deck lo lanza con el gestor predeterminado del sistema.

Solo funciona con **Macro Deck 3**. No es compatible con Macro Deck 2.x.

Un pulsador abre una aplicación o archivo:

- `.exe` — lanzado directamente en Windows
- `.txt`, `.pdf` y otros archivos — abiertos con la aplicación asociada por defecto
- La ruta del ejecutable se configura por instancia de botón
- Funciona en Windows, Linux y macOS

## Características

- Abre cualquier aplicación local por su ruta de ejecutable
- Abre documentos con la aplicación asociada por defecto
- Un solo parámetro de texto para la ruta del ejecutable o archivo
- Puntos de entrada multiplataforma: Windows (win-x64), Linux (linux-x64), macOS (osx-arm64)
- Dependiente del framework: artefacto pequeño (~0,7 MB por plataforma); Macro Deck 3 provee el runtime de .NET 10

## Requisitos

- Macro Deck 3.0.0 o superior
- Windows 10/11, Linux o macOS

## Instalación

El plugin se distribuye como un paquete `.macroDeckPlugin`:

```
com.macrodeck.openapp-1.0.0.macroDeckPlugin
```

Está en la sección **Releases**. Descárgalo desde ahí.

### Instalar en Macro Deck 3

Macro Deck 3 tiene una sección integrada de **Plugins** para añadir plugins de terceros:

1. Abre Macro Deck 3.
2. Ve a la sección **Plugins** (o Extensiones, según tu compilación).
3. Busca la opción para instalar o añadir un plugin desde un archivo local (por ejemplo, un botón que diga "Install plugin" o "Add plugin").
4. Selecciona el archivo `.macroDeckPlugin`:

   ```
   com.macrodeck.openapp-1.0.0.macroDeckPlugin
   ```

5. Sigue los indicadores de pantalla para terminar la instalación.
6. Reinicia Macro Deck 3 si te lo pide.

La documentación oficial de Macro Deck no publica una guía paso a paso de la interfaz para instalar archivos `.macroDeckPlugin` locales. Si no ves la opción en tu compilación, consulta la ayuda de la aplicación o los recursos de la comunidad de Macro Deck para saber dónde está el menú exacto en tu versión.

### Qué contiene el archivo `.macroDeckPlugin`

Es un archivo ZIP con todo lo que el plugin necesita:

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

Como es dependiente del framework, Macro Deck 3 suministra el runtime de .NET 10, así que el artefacto solo trae los ensamblados del plugin — nada de bundles grandes del runtime.

## Uso

1. Entra en **Modo Edición** en Macro Deck 3.
2. Añade un botón nuevo o edita uno existente.
3. En el selector de acciones, elige **"Open App"** (Abrir Aplicación).
4. Configura la acción:
   - **Ruta del ejecutable**: pon la ruta completa al `.exe` o archivo que quieras abrir (por ejemplo, `C:\Windows\notepad.exe` en Windows, `/usr/bin/firefox` en Linux, `/Applications/Safari.app` en macOS).
5. Guarda el botón y sal del Modo Edición.
6. Pulsa el botón — se abrirá la aplicación o el archivo.

## Desarrollo

El código fuente del plugin está aquí:

```
/home/sparky/Documentos/Macro Deck proyect scripts/Macro Deck proyect scritps/OpenAppPlugin/
```

### Estructura del proyecto

| Archivo | Para qué sirve |
|---------|----------------|
| `manifest.json` | Identidad del plugin, versión, puntos de entrada por plataforma |
| `Program.cs` | Punto de entrada; registra la integración e inicia el host del plugin |
| `PluginIntegration.cs` | Declara la acción `Open App` |
| `OpenAppAction.cs` | Definición y ejecutor de la acción (abre la ruta del ejecutable) |
| `Localization/Strings.resx` | Cadenas localizadas (español por defecto) |
| `Assets/icon.svg` | Icono del plugin |
| `macrodeck-build.json` | Instrucciones de compilación por plataforma |
| `OpenAppPlugin.csproj` | Proyecto .NET 10, dependiente del framework, ASP.NET Core |

### Compilar

```bash
dotnet tool install --global MacroDeck.Plugin.Cli --prerelease

macrodeck-plugin build --output ../../artifacts
```

Esto genera `com.macrodeck.openapp-1.0.0.macroDeckPlugin` en la carpeta `artifacts`.

### Validar

```bash
macrodeck-plugin validate --artifact artifacts/com.macrodeck.openapp-1.0.0.macroDeckPlugin
```

## Compatibilidad

| Plataforma | Soportada |
|------------|-----------|
| Windows | Sí |
| Linux | Sí |
| macOS | Sí |
| Macro Deck 3 | Sí |
| Macro Deck 2.x | No |

## Licencia

MIT License

## Fuentes

- [Macro Deck Docs](https://docs.macro-deck.app/)
- [Macro Deck User Guide](https://docs.macro-deck.app/guide/)
- [Macro Deck Plugin CLI](https://docs.macro-deck.app/cli/)
- [Macro Deck Quickstart](https://docs.macro-deck.app/introduction/quickstart/)
