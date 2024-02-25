using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Maestro.Services;

namespace Maestro.ViewModels;

public partial class ResourceContentViewModel : ViewModelBase
{
    readonly AppServices _appServices;

    public ResourceContentViewModel(AppServices appServices)
    {
        _appServices = appServices;
    }

    [ObservableProperty]
    private string? _title;

    [ObservableProperty]
    private string? _text;

    [RelayCommand]
    private void Close()
    {
        _appServices.OpenResourceManager.Close(this.Title);
    }
}
