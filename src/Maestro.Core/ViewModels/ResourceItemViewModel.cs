using CommunityToolkit.Mvvm.Input;
using Maestro.Core.Services.Contracts;
using System.Threading.Tasks;

namespace Maestro.Core.ViewModels;

public enum ResourceKind
{
    Resource,
    Folder
}

public partial class ResourceItemViewModel : AbstractResourceItemViewModel
{
    readonly IOpenDocumentManager _openResManager;

    public ResourceItemViewModel(IOpenDocumentManager openResManager) : base()
    {
        _openResManager = openResManager;
        this.Name = string.Empty;
        this.Type = string.Empty;
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
        await _openResManager.OpenDocumentAsync(this.Name!, content);
    }
}