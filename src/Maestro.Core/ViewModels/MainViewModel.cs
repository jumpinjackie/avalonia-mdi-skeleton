using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Maestro.Services;
using Maestro.Services.Messaging;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Maestro.ViewModels;

public partial class MainViewModel : ViewModelBase, IRecipient<OpenResourceMessage>, IRecipient<CloseResourceMessage>
{
    readonly AppServices _appServices;

    // Just to shut up designer errors
    public MainViewModel() { }

    public MainViewModel(SidebarViewModel sidebar, AppServices appServices)
    {
        this._sidebar = sidebar;
        _appServices = appServices;
        this.IsActive = true;
    }

    [ObservableProperty]
    private SidebarViewModel _sidebar;

    public ObservableCollection<ResourceContentViewModel> OpenResources { get; } = new();

    [RelayCommand]
    private async Task ConnectToSite()
    {
        await _appServices.ConnectionManager.ConnectAsync();
    }

    [ObservableProperty]
    private int _openResourceIndex;

    void IRecipient<OpenResourceMessage>.Receive(OpenResourceMessage message)
    {
        var ores = FindOpenResource(message.Name);
        if (ores != null)
        {
            this.OpenResourceIndex = this.OpenResources.IndexOf(ores);
        }
        else
        {
            var rvm = new ResourceContentViewModel(_appServices)
            {
                Title = message.Name,
                Text = message.Content
            };
            this.OpenResources.Add(rvm);
            this.OpenResourceIndex = this.OpenResources.Count - 1;
        }
    }

    private ResourceContentViewModel? FindOpenResource(string name) => this.OpenResources.FirstOrDefault(or => or.Title == name);

    void IRecipient<CloseResourceMessage>.Receive(CloseResourceMessage message)
    {
        var ores = FindOpenResource(message.Name);
        if (ores != null)
        {
            this.OpenResources.Remove(ores);
        }
    }
}
