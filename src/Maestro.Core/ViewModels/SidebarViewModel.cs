using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Maestro.Core.Services.Messaging;
using Maestro.Core.Services.Stubs;
using System;
using System.Collections.ObjectModel;

namespace Maestro.Core.ViewModels;

public partial class SidebarViewModel : RecipientViewModelBase, IRecipient<ConnectedToSiteMessage>, IRecipient<CancelConnectMessage>
{
    readonly Func<FolderItemViewModel> _createFolderModel;
    readonly Func<ResourceItemViewModel> _createResourceModel;

    [ObservableProperty]
    private ConnectViewModel _connect;

    // Designer-only ctor
    public SidebarViewModel()
    {
        _createFolderModel = () => new FolderItemViewModel();
        _createResourceModel = () => new ResourceItemViewModel(new StubOpenDocumentManager(WeakReferenceMessenger.Default));
        _connect = new ConnectViewModel(new StubConnectionManager(WeakReferenceMessenger.Default));
    }

    public SidebarViewModel(Func<FolderItemViewModel> createFolderModel,
                            Func<ResourceItemViewModel> createResourceModel,
                            ConnectViewModel connectViewModel)
    {
        _createFolderModel = createFolderModel;
        _createResourceModel = createResourceModel;
        _connect = connectViewModel;
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
        var svm = new SiteViewModel(message.Name);

        foreach (var f in message.Root.Folders)
        {
            svm.Children.Add(_createFolderModel().WithName(f.Name));
        }
        foreach (var r in message.Root.Resources)
        {
            svm.Children.Add(_createResourceModel().WithNameAndType(r.Name, r.Type));
        }

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
