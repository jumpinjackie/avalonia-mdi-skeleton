using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Maestro.Core.Services.Contracts;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Maestro.Core.ViewModels;

public enum ResourceKind
{
    Resource,
    Folder
}

public abstract partial class AbstractResourceItemViewModel : ViewModelBase
{
    protected AbstractResourceItemViewModel()
    {
        this.Icon = "document_page_top_left_regular";
    }

    // TODO: Not actually used, but figure out how to bind <PathIcon /> to use this resource reference
    public string Icon { get; }

    [ObservableProperty]
    private string? _name;

    public abstract ResourceKind Kind { get; }
}

public partial class ResourceItemViewModel : AbstractResourceItemViewModel
{
    readonly IOpenDocumentManager _openResManager;

    public ResourceItemViewModel(IOpenDocumentManager openResManager) : base()
    {
        _openResManager = openResManager;
    }

    public ResourceItemViewModel WithNameAndType(string name, string type)
    {
        this.Name = name;
        this.Type = type;
        return this;
    }

    public string Type { get; private set; }

    public override ResourceKind Kind => ResourceKind.Resource;

    [RelayCommand]
    private async Task Open()
    {
        object content = $"Content for [Name: {this.Name}, Type: {this.Type}]";
        await _openResManager.OpenDocumentAsync(this.Name, content);
    }
}

public interface IFolderItemViewModel
{
    ObservableCollection<AbstractResourceItemViewModel> Children { get; }
}

public partial class FolderItemViewModel : AbstractResourceItemViewModel, IFolderItemViewModel
{
    public FolderItemViewModel() : base()
    {
    }

    public FolderItemViewModel WithName(string name)
    {
        this.Name = name;
        return this;
    }

    [ObservableProperty]
    private bool _isExpanded;

    public ObservableCollection<AbstractResourceItemViewModel> Children { get; } = new();

    public override ResourceKind Kind => ResourceKind.Folder;
}