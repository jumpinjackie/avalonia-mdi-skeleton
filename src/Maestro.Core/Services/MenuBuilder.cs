using Maestro.Core.ViewModels;
using System.Collections.Generic;

namespace Maestro.Core.Services;

public class MenuBuilder
{
    public MenuBuilder() { }

    public IReadOnlyList<MenuItemViewModel> Build(MainViewModel vm)
    {
        return [
            new MenuItemViewModel {
                Header = "_File",
                Items = [
                    new MenuItemViewModel { Header = "_New Resource" },
                    new MenuItemViewModel { Header = "-" },
                    new MenuItemViewModel { Header = "Save Resource" },
                    new MenuItemViewModel { Header = "Save Resource As" },
                    new MenuItemViewModel { Header = "Save All" },
                    new MenuItemViewModel { Header = "-" },
                    new MenuItemViewModel { Header = "_Exit" }
                ] 
            },
            new MenuItemViewModel {
                Header = "_Edit",
                Items = [
                    new MenuItemViewModel { Header = "Copy Resource ID to clipboard" },
                    new MenuItemViewModel { Header = "-" },
                    new MenuItemViewModel { Header = "Copy" },
                    new MenuItemViewModel { Header = "Cut" },
                    new MenuItemViewModel { Header = "Paste" }
                ]
            },
            new MenuItemViewModel {
                Header = "_Tools",
                Items = [
                    new MenuItemViewModel { Header = "Site Administrator" },
                    new MenuItemViewModel { Header = "Feature Source Preview" },
                    new MenuItemViewModel { Header = "Server Status Monitor" },
                    new MenuItemViewModel {
                        Header = "_Package",
                        Items = [
                            new MenuItemViewModel { Header = "Create Package" },
                            new MenuItemViewModel { Header = "Load Package" },
                            new MenuItemViewModel { Header = "Package Editor" }
                        ]
                    },
                    new MenuItemViewModel { Header = "-" },
                    new MenuItemViewModel { Header = "Options", Command = vm.OptionsCommand }
                ]
            },
            new MenuItemViewModel {
                Header = "_Help",
                Items = [
                    new MenuItemViewModel { Header = "User Guide" },
                    new MenuItemViewModel { Header = "Show Welcome Tab", Command = vm.WelcomeCommand },
                    new MenuItemViewModel { Header = "-" },
                    new MenuItemViewModel { Header = "About MapGuide Maestro" }
                ]
            }
        ];
    }
}
