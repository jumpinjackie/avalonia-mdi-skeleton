# Avalonia MDI Skeleton

[![Actions Status](https://github.com/jumpinjackie/avalonia-mdi-skeleton/workflows/.NET/badge.svg)](https://github.com/jumpinjackie/avalonia-mdi-skeleton/actions)

This repo provides a minimal and multi-platform [MDI (Multi-Document Interface)](https://en.wikipedia.org/wiki/Multiple-document_interface) skeleton desktop application built on-top of [Avalonia](https://www.avaloniaui.net/)

This skeleton app implements a hypothetical new user interface for the existing [MapGuide Maestro](https://github.com/jumpinjackie/mapguide-maestro) application

# Features

 * Uses Avalonia 11 and targets .net 8.0
 * Main UI layout in the style of applicationss like:
    * [Visual Studio Code](https://code.visualstudio.com/)
    * [Azure Data Studio](https://azure.microsoft.com/en-us/products/data-studio)
    * [Azure Storage Explorer](https://azure.microsoft.com/en-us/products/storage/storage-explorer)
 * Supports Desktop and Browser WASM (Web Assembly) targets
    * Check out the WASM build of this application [here](https://jumpinjackie.github.io/avalonia-mdi-skeleton/master/index.html)
       * Requires a browser that supports [Web Assembly](https://caniuse.com/wasm)
 * Strong usage of MVVM (via `CommunityToolkit.Mvvm`) and data-binding patterns
    * Usage of [IMessenger](https://learn.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/messenger) and `IRecipient<T>` for inter-ViewModel communication
 * Dependency Injection via [StrongInject](https://github.com/YairHalberstadt/stronginject) (chosen for its source-generator and compile-time nature, which should make it more friendly for app trimming scenarios should we ever want to make a trimmed single-file published build)
 * Dynamically constructed and bound app menu (see `MenuBuilder` class) to facilitate potential future augementation of the final app menu via plugins.
 * Mimics the core functionalities of [MapGuide Maestro](https://github.com/jumpinjackie/mapguide-maestro)
    * Presenting a login UI for connecting to a hypothetical [MapGuide](https://www.osgeo.org/projects/mapguide-open-source/) Server with mocked authentication
    * Once connected, provides a mocked and randomized tree view of a MapGuide Server's resources in a directory-like structure
       * Folder items can be opened to load a random mixture of child folders and resource items on demand.
       * Resource items can be double-clicked to open a (closeable) resource editor tab for that resource
       * Resource editor tabs embed the [AvaloniaEdit](https://github.com/AvaloniaUI/AvaloniaEdit) editor component
    * This conceptual model should be easily translatable to other similar concepts, such as:
       * Connecting to a database, to explore schemas/tables/columns in a tree view
       * Opening a directory, to explore its files and subdirectories in a tree view
 * Demonstrates binding of polymorphic tab view models on the main `TabControl` via `<UserControl.DataTemplates>`. The main TabControl can show
    * Resource editor tabs
    * Welcome tab (shown on startup or `Help - Show Welcome Screen`)
    * Options tab (shown on `Tools - Options`)
 * Includes a GitHub Actions workflow that automatically:
    * Produces self-contained published builds for Windows/Linux/OSX/WASM. Artifacts uploaded and attached for each build.
    * Also produces an [AppImage](https://appimage.org/) for Linux (thanks to [PupNet Deploy](https://github.com/kuiperzone/PupNet-Deploy))
    * Automatically pushes WASM builds to GitHub Pages `gh-pages` branch
    * Produces a release (with respective build artifacts) in draft/prerelease state on git tag.

# TODO

 * [ ] Fix sidebar treeview not presenting scrollbars on overflow despite the designer suggesting (at design-time) that this should already be working
 * [ ] Basic persistent app options/settings system
    * Theme (light/dark)
    * Selected Language
 * [x] i18n
 * [ ] Basic test suite
    * [ ] CI: Code coverage generation and reporting
 * [ ] CI: Changelog generation
 * [ ] CI: Release notes generation

# Known Issues/Limitations

 * The resx-based i18n string bundles do not work on the WASM target. Regardless of whatever culture you assign on startup, the language will always be the default (English).
 * Node action buttons on the main tree view should only be displaying for the actively hovered node, but currently displays it for the hovered node *and all of its parent folder nodes*. I can't seem to nail down the correct selector (if this is even possible) to only cover action buttons for the actively hovered node.
 * The choice of packages used and patterns being employed are mostly constrained by our desire for a functional WASM target.
    * For example, I wanted to use [this library](https://github.com/mysteryx93/HanumanInstitute.MvvmDialogs) for easy dialogs, but it didn't work in a WASM environment.
 * This author is not an expert on WPF or any other XAML-based frameworks and their best practices and so may be doing some things wrong 
    * Feel free to correct me through pull requests 😉