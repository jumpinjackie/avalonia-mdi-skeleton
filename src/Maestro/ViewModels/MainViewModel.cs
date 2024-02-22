using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Maestro.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private SidebarViewModel sidebar = new();

    public ObservableCollection<ResourceContentViewModel> OpenResources { get; } = new ObservableCollection<ResourceContentViewModel>();

    private int _counter = 0;

    [RelayCommand]
    private void OpenResource()
    {
        new ResourceContentViewModel(this.OpenResources)
        {
            Title = "New Resource " + (_counter++),
            Text = $"Content for (New Resource {_counter})"
        }.Add();
    }

    [RelayCommand]
    private Task ConnectToSite()
    {
        return this.Sidebar.ConnectToSiteAsync();
    }
}
