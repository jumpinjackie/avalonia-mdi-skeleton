using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Maestro.Core.Services.Contracts;
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
        this._name = name;
    }

    [ObservableProperty]
    private string _name;

    public abstract ResourceKind Kind { get; }
}

public partial class ResourceItemViewModel : AbstractResourceItemViewModel
{
    readonly IOpenResourceManager _openResManager;

    public ResourceItemViewModel(string name, IOpenResourceManager openResManager) : base(name)
    {
        _openResManager = openResManager;
    }

    public override ResourceKind Kind => ResourceKind.Resource;

    [RelayCommand]
    private async Task Open()
    {
        await _openResManager.OpenResourceAsync(this.Name);
    }
}

public partial class FolderItemViewModel : AbstractResourceItemViewModel
{
    public FolderItemViewModel(string name) : base(name)
    {
    }

    [ObservableProperty]
    private bool _isExpanded;

    public ObservableCollection<AbstractResourceItemViewModel> Children { get; } = new();

    public override ResourceKind Kind => ResourceKind.Folder;
}