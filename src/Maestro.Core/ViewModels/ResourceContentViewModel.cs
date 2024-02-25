using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Maestro.Core.Services.Contracts;

namespace Maestro.Core.ViewModels;

public partial class ResourceContentViewModel : ViewModelBase
{
    readonly IOpenResourceManager _openResManager;

    public ResourceContentViewModel(IOpenResourceManager openResManager)
    {
        _openResManager = openResManager;
    }

    [ObservableProperty]
    private string? _title;

    [ObservableProperty]
    private string? _text;

    [RelayCommand]
    private void Close()
    {
        _openResManager.Close(this.Title);
    }
}
