using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Maestro.Services;
using Maestro.Services.Messaging;
using System.Collections.ObjectModel;

namespace Maestro.ViewModels;

public partial class SidebarViewModel : ViewModelBase, IRecipient<ConnectedToSiteMessage>
{
    readonly AppServices _appServices;

    public SidebarViewModel(AppServices appServices)
    {
        _appServices = appServices;
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
            svm.Children.Add(new FolderItemViewModel(f.Name));
        }
        foreach (var r in message.Root.Resources)
        {
            svm.Children.Add(new ResourceItemViewModel(r.Name, _appServices));
        }

        this.ConnectedSites.Add(svm);
        if (this.ActiveSite == null)
            this.ActiveSite = svm;
    }
}
