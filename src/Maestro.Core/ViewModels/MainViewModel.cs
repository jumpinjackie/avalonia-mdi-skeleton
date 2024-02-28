using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Maestro.Core.Services.Contracts;
using Maestro.Core.Services.Messaging;
using Maestro.Core.Services.Stubs;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Maestro.Core.ViewModels;

public partial class MainViewModel : RecipientViewModelBase, IRecipient<OpenDocumentMessage>, IRecipient<CloseDocumentMessage>
{
    readonly IConnectionManager _connManager;
    readonly Func<ResourceContentViewModel> _createResourceContent;
    readonly Func<WelcomeViewModel> _createWelcome;

    // Designer-only ctor
    public MainViewModel() 
    {
        _connManager = new StubConnectionManager(WeakReferenceMessenger.Default);
        var openDocManager = new StubOpenDocumentManager(WeakReferenceMessenger.Default);
        _createResourceContent = () => new ResourceContentViewModel(openDocManager);
        _createWelcome = () => new WelcomeViewModel(openDocManager);
        _sidebar = new SidebarViewModel(
            name => new FolderItemViewModel(name),
            name => new ResourceItemViewModel(name, openDocManager),
            new ConnectViewModel(_connManager));
        this.IsActive = true;
    }

    public MainViewModel(SidebarViewModel sidebar,
                         IConnectionManager connManager,
                         Func<ResourceContentViewModel> createResourceContent,
                         Func<WelcomeViewModel> createWelcomeModel)
    {
        _sidebar = sidebar;
        _connManager = connManager;
        _createResourceContent = createResourceContent;
        _createWelcome = createWelcomeModel;
        this.IsActive = true;
    }

    [ObservableProperty]
    private SidebarViewModel _sidebar;

    public ObservableCollection<TabDocumentViewModel> OpenTabs { get; } = new();

    [ObservableProperty]
    private int _openTabIndex;

    protected override void OnActivated()
    {
        base.OnActivated();
        this.OpenTabs.Add(_createWelcome());
    }

    void IRecipient<OpenDocumentMessage>.Receive(OpenDocumentMessage message)
    {
        var ores = FindOpenTab(message.Name);
        if (ores != null)
        {
            this.OpenTabIndex = this.OpenTabs.IndexOf(ores);
        }
        else
        {
            var rvm = _createResourceContent();
            rvm.Title = message.Name;
            this.OpenTabs.Add(rvm);
            this.OpenTabIndex = this.OpenTabs.Count - 1;
        }
    }

    private TabDocumentViewModel? FindOpenTab(string name) => this.OpenTabs.FirstOrDefault(or => or.Title == name);

    void IRecipient<CloseDocumentMessage>.Receive(CloseDocumentMessage message)
    {
        var ores = FindOpenTab(message.Name);
        if (ores != null)
        {
            this.OpenTabs.Remove(ores);
        }
    }
}
