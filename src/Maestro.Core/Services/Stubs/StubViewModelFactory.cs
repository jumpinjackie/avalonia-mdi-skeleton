using CommunityToolkit.Mvvm.Messaging;
using Maestro.Core.Services.Contracts;
using Maestro.Core.ViewModels;
using System;

namespace Maestro.Core.Services.Stubs;

internal class StubViewModelFactory : IViewModelFactory
{
    readonly Func<FolderItemViewModel> _folderItem;
    readonly Func<ResourceItemViewModel> _resourceItem;
    readonly Func<ResourceContentViewModel> _resourceContent;
    readonly Func<WelcomeViewModel> _welcome;
    readonly Func<OptionsViewModel> _options;
    readonly Func<ConnectViewModel> _connect;
    readonly Func<SiteViewModel> _site;

    readonly StubConnectionManager _connManager;
    readonly StubOpenDocumentManager _docManager;

    public StubViewModelFactory()
    {
        _connManager = new StubConnectionManager(WeakReferenceMessenger.Default);
        _docManager = new StubOpenDocumentManager(WeakReferenceMessenger.Default);

        _folderItem = () => new FolderItemViewModel();
        _resourceItem = () => new ResourceItemViewModel(_docManager);
        _resourceContent = () => new ResourceContentViewModel(_docManager);
        _welcome = () => new WelcomeViewModel(_docManager);
        _options = () => new OptionsViewModel(_docManager);
        _connect = () => new ConnectViewModel(_connManager);
        _site = () => new SiteViewModel(this);
    }

    public FolderItemViewModel FolderItem() => _folderItem();

    public ResourceItemViewModel ResourceItem() => _resourceItem();

    public ResourceContentViewModel ResourceContent() => _resourceContent();

    public WelcomeViewModel Welcome() => _welcome();

    public OptionsViewModel Options() => _options();

    public ConnectViewModel Connect() => _connect();

    public SiteViewModel Site() => _site();
}
