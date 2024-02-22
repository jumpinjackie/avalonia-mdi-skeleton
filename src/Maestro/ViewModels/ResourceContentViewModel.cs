using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Maestro.ViewModels;

public partial class ResourceContentViewModel : ViewModelBase
{
    readonly ObservableCollection<ResourceContentViewModel> _parentCollection;

    public ResourceContentViewModel(ObservableCollection<ResourceContentViewModel> parentCollection)
    {
        _parentCollection = parentCollection;
    }

    internal void Add()
    {
        _parentCollection.Add(this);
    }

    [ObservableProperty]
    private string? title;

    [ObservableProperty]
    private string? text;

    [RelayCommand]
    private void Close()
    {
        _parentCollection.Remove(this);
    }
}
