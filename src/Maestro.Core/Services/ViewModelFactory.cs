using Maestro.Core.Services.Contracts;
using Maestro.Core.ViewModels;
using System;

namespace Maestro.Core.Services;

public class ViewModelFactory : IViewModelFactory
{
    readonly Func<FolderItemViewModel> _folderItem;
    readonly Func<ResourceItemViewModel> _resourceItem;
    readonly Func<ResourceContentViewModel> _resourceContent;
    readonly Func<WelcomeViewModel> _welcome;
    readonly Func<OptionsViewModel> _options;
    readonly Func<ConnectViewModel> _connect;
    readonly Func<SiteViewModel> _site;

    public ViewModelFactory(Func<FolderItemViewModel> folderItem,
                            Func<ResourceItemViewModel> resourceItem,
                            Func<ResourceContentViewModel> resourceContent,
                            Func<WelcomeViewModel> welcome,
                            OptionsViewModel options,
                            ConnectViewModel connect,
                            Func<SiteViewModel> site)
    {
        _folderItem = folderItem;
        _resourceItem = resourceItem;
        _resourceContent = resourceContent;
        _welcome = welcome;
        _options = () => options;
        _connect = () => connect;
        _site = site;
    }

    public FolderItemViewModel FolderItem() => _folderItem();

    public ResourceItemViewModel ResourceItem() => _resourceItem();

    public ResourceContentViewModel ResourceContent() => _resourceContent();

    public WelcomeViewModel Welcome() => _welcome();

    public OptionsViewModel Options() => _options();

    public ConnectViewModel Connect() => _connect();

    public SiteViewModel Site() => _site();
}
