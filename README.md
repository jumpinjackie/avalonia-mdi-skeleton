# Avalonia MDI Skeleton

[![Actions Status](https://github.com/jumpinjackie/avalonia-mdi-skeleton/workflows/.NET/badge.svg)](https://github.com/jumpinjackie/avalonia-mdi-skeleton/actions)

This repo provides a minimal and multi-platform MDI (Multi-Document Interface) skeleton application built on-top of Avalonia

This skeleton app implements a hypothetical new user interface for the existing [MapGuide Maestro](https://github.com/jumpinjackie/mapguide-maestro)

# Features

 * Uses Avalonia 11 and targets .net 8.0
 * Main UI layout resembling applications like:
	* [Visual Studio Code](https://code.visualstudio.com/)
	* [Azure Data Studio](https://azure.microsoft.com/en-us/products/data-studio)
	* [Azure Storage Explorer](https://azure.microsoft.com/en-us/products/storage/storage-explorer)
 * Supports Desktop and Browser WASM (Web Assembly) targets
	* Check out the WASM build of this application [here](https://jumpinjackie.github.io/avalonia-mdi-skeleton/master/index.html)
	   * Requires a browser that supports [Web Assembly](https://caniuse.com/wasm)
 * Strong usage of MVVM (via `CommunityToolkit.Mvvm`) and data-binding patterns
	* Usage of [IMessenger](https://learn.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/messenger) and `IRecipient<T>` for inter-ViewModel communication
 * Dependency Injection via StrongInject (chosen for its source-generator and compile-time nature, making it friendly for app trimming scenarios)
 * Dynamically constructed and bound app menu (see `MenuBuilder`) to facilitate potential future augementation via plugins.
 * Mimics the core functionalities of MapGuide Maestro
	* Present a login UI for connecting to a MapGuide Server with mocked authentication
	* Once connected, provides a tree view of a MapGuide Server's resources
	   * Resource items can be double-clicked to open a (closeable) resource editor tab for that resource
	   * Resource editor tabs embed the [AvaloniaEdit](https://github.com/AvaloniaUI/AvaloniaEdit) editor component
    * This conceptual model should be easily translatable to other similar concepts, such as:
	   * Connecting to a database, to explore schemas/tables/columns in a tree view
	   * Opening a directory, to explore its files and subdirectories in a tree view
 * Demonstrates binding of polymorphic tab view models on the main TabControl via `<UserControl.DataTemplates>`. The main TabControl can show
	* Resource editor tabs
	* Welcome tab (shown on startup or `Help - Show Welcome Screen`)
	* Options tab (shown on `Tools - Options`)
 * Includes GitHub Actions workflow that automatically:
	* Produces self-contained published builds for Windows/Linux/OSX/WASM. Artifacts uploaded and attached for each build.
	* Also produces an AppImage for Linux (thanks to [PupNet Deploy](https://github.com/kuiperzone/PupNet-Deploy))
	* Automatically pushes WASM builds to GitHub Pages `gh-pages` branch

# Known Issues/Limitations

 * Double-clicking of resource item tree nodes (to open an editor tab) is not precise. Still trying to figure out to how reliably handle "double clicking" of TreeView nodes.
 * WASM target support is a driving factor in determining what packages we're using.
	* For example, I wanted to use [this library](https://github.com/mysteryx93/HanumanInstitute.MvvmDialogs) for easy dialogs, but it didn't work in a WASM environment.
 * This author is not an expert on XAML-based frameworks and their best practices and so may be doing some things wrong (feel free to correct me through pull requests :))