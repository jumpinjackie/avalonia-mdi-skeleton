using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Maestro.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Maestro.ViewModels;

public enum ResourceKind
{
    Resource,
    Folder
}

public abstract partial class AbstractResourceItemViewModel : ViewModelBase
{
    protected AbstractResourceItemViewModel(string name)
    {
        this.name = name;
    }

    [ObservableProperty]
    private string name;

    public abstract ResourceKind Kind { get; }
}

public partial class ResourceItemViewModel : AbstractResourceItemViewModel
{
    readonly AppServices _appServices;

    public ResourceItemViewModel(string name, AppServices orManager) : base(name)
    {
        _appServices = orManager;
    }

    public override ResourceKind Kind => ResourceKind.Resource;

    [RelayCommand]
    private async Task Open()
    {
        await _appServices.OpenResourceManager.OpenResourceAsync(this.Name);
    }
}

public partial class FolderItemViewModel : AbstractResourceItemViewModel
{
    public FolderItemViewModel(string name) : base(name)
    {
    }

    [ObservableProperty]
    private bool isExpanded;

    public ObservableCollection<AbstractResourceItemViewModel> Children { get; } = new();

    public override ResourceKind Kind => ResourceKind.Folder;
}