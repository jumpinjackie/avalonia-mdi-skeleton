using Maestro.Core.Assets;
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
                Header = Resources.Menu_File,
                Items = [
                    new MenuItemViewModel { Header = Resources.Cmd_NewResource, IsEnabled = false },
                    new MenuItemViewModel { Header = "-" },
                    new MenuItemViewModel { Header = Resources.Cmd_SaveResource, IsEnabled = false },
                    new MenuItemViewModel { Header = Resources.Cmd_SaveResourceAs, IsEnabled = false },
                    new MenuItemViewModel { Header = Resources.Cmd_SaveAll, IsEnabled = false },
                    new MenuItemViewModel { Header = "-" },
                    new MenuItemViewModel { Header = Resources.Cmd_Exit, IsEnabled = false }
                ] 
            },
            new MenuItemViewModel {
                Header = Resources.Menu_Edit,
                Items = [
                    new MenuItemViewModel { Header = Resources.Cmd_CopyResourceId, IsEnabled = false },
                    new MenuItemViewModel { Header = "-" },
                    new MenuItemViewModel { Header = Resources.Common_Copy, IsEnabled = false },
                    new MenuItemViewModel { Header = Resources.Common_Cut, IsEnabled = false },
                    new MenuItemViewModel { Header = Resources.Common_Paste, IsEnabled = false }
                ]
            },
            new MenuItemViewModel {
                Header = Resources.Menu_Tools,
                Items = [
                    new MenuItemViewModel { Header = Resources.Cmd_SiteAdmin, IsEnabled = false },
                    new MenuItemViewModel { Header = Resources.Cmd_FsPreview, IsEnabled = false },
                    new MenuItemViewModel { Header = Resources.Cmd_ServerMon, IsEnabled = false },
                    new MenuItemViewModel {
                        Header = Resources.Menu_Tools_Package,
                        Items = [
                            new MenuItemViewModel { Header = Resources.Cmd_CreatePackage, IsEnabled = false },
                            new MenuItemViewModel { Header = Resources.Cmd_LoadPackage, IsEnabled = false },
                            new MenuItemViewModel { Header = Resources.Cmd_PackageEditor, IsEnabled = false }
                        ]
                    },
                    new MenuItemViewModel { Header = "-" },
                    new MenuItemViewModel { Header = Resources.Common_Options, Command = vm.OptionsCommand, IsEnabled = true }
                ]
            },
            new MenuItemViewModel {
                Header = Resources.Common_Help,
                Items = [
                    new MenuItemViewModel { Header = Resources.Welcome_UserGuide, IsEnabled = false },
                    new MenuItemViewModel { Header = Resources.Cmd_ShowWelcomeTab, Command = vm.WelcomeCommand, IsEnabled = true },
                    new MenuItemViewModel { Header = "-" },
                    new MenuItemViewModel { Header = Resources.Common_About, IsEnabled = false }
                ]
            }
        ];
    }
}
