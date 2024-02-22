using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Maestro.ViewModels;

public partial class SidebarViewModel : ViewModelBase
{
    [ObservableProperty]
    private SiteViewModel? activeSite;

    public ObservableCollection<SiteViewModel> ConnectedSites { get; } = new ObservableCollection<SiteViewModel>();

    private int _siteCounter = 0;

    internal async Task ConnectToSiteAsync()
    {
        var name = "MapGuide Site " + _siteCounter++;
        var svm = new SiteViewModel(this, name);
        await svm.AddAsync();
        if (this.ActiveSite == null)
            this.ActiveSite = svm;
    }
}
