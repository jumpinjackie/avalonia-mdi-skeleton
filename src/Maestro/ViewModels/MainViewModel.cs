using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Maestro.ViewModels;

public partial class MainViewModel : ViewModelBase
{
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
}
