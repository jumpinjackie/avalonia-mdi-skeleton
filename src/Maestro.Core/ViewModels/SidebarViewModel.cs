using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Maestro.Core.Services.Messaging;
using Maestro.Core.Services.Stubs;
using System;
using System.Collections.ObjectModel;

namespace Maestro.Core.ViewModels;

public partial class SidebarViewModel : ViewModelBase, IRecipient<ConnectedToSiteMessage>
{
    readonly Func<string, FolderItemViewModel> _createFolderModel;
    readonly Func<string, ResourceItemViewModel> _createResourceModel;

    // Designer-only ctor
    public SidebarViewModel()
    {
        _createFolderModel = name => new FolderItemViewModel(name);
        _createResourceModel = name => new ResourceItemViewModel(name, new StubOpenResourceManager());
    }

    public SidebarViewModel(Func<string, FolderItemViewModel> createFolderModel,
                            Func<string, ResourceItemViewModel> createResourceModel)
    {
        _createFolderModel = createFolderModel;
        _createResourceModel = createResourceModel;
        this.IsActive = true;
    }

    [ObservableProperty]
    private SiteViewModel? _activeSite;

    public ObservableCollection<SiteViewModel> ConnectedSites { get; } = new();

    void IRecipient<ConnectedToSiteMessage>.Receive(ConnectedToSiteMessage message)
    {
        var svm = new SiteViewModel(message.Name);

        foreach (var f in message.Root.Folders)
        {
            svm.Children.Add(_createFolderModel(f.Name));
        }
        foreach (var r in message.Root.Resources)
        {
            svm.Children.Add(_createResourceModel(r.Name));
        }

        this.ConnectedSites.Add(svm);
        if (this.ActiveSite == null)
            this.ActiveSite = svm;
    }
}
