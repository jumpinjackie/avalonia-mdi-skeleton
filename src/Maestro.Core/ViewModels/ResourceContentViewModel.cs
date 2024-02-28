using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Maestro.Core.Services.Contracts;

namespace Maestro.Core.ViewModels;

public partial class ResourceContentViewModel : TabDocumentViewModel
{
    public ResourceContentViewModel(IOpenDocumentManager openDocManager)
        : base(openDocManager)
    { }

    [ObservableProperty]
    private string? _text;
}
