using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Maestro.Core.Services.Contracts;
using Maestro.Core.Services.Messaging;
using Maestro.Core.Services.Stubs;
using System.Collections.ObjectModel;

namespace Maestro.Core.ViewModels;

public partial class SidebarViewModel : RecipientViewModelBase, IRecipient<ConnectedToSiteMessage>, IRecipient<CancelConnectMessage>
{
    readonly IViewModelFactory _vmFactory;

    [ObservableProperty]
    private ConnectViewModel _connect;

    // Designer-only ctor
    public SidebarViewModel()
    {
        _vmFactory = new StubViewModelFactory();
        _connect = _vmFactory.Connect();
        this.IsActive = true;
    }

    public SidebarViewModel(IViewModelFactory vmFactory)
    {
        _vmFactory = vmFactory;
        _connect = vmFactory.Connect();
        this.IsActive = true;
    }

    [RelayCommand]
    private void BeginConnect()
    {
        this.IsConnecting = true;
    }

    [ObservableProperty]
    private bool _isConnecting;

    [ObservableProperty]
    private SiteViewModel? _activeSite;

    public ObservableCollection<SiteViewModel> ConnectedSites { get; } = new();

    void IRecipient<ConnectedToSiteMessage>.Receive(ConnectedToSiteMessage message)
    {
        var svm = _vmFactory.Site();
        svm.SiteName = message.SiteName;
        svm.PopulateFolder(message.SiteName, message.Root, svm);

        this.ConnectedSites.Add(svm);
        if (this.ActiveSite == null)
            this.ActiveSite = svm;

        this.IsConnecting = false;
    }

    void IRecipient<CancelConnectMessage>.Receive(CancelConnectMessage message)
    {
        this.IsConnecting = false;
    }
}
