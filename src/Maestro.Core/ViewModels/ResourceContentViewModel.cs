using AvaloniaEdit.Document;
using CommunityToolkit.Mvvm.ComponentModel;
using Maestro.Core.Services.Contracts;

namespace Maestro.Core.ViewModels;

public partial class ResourceContentViewModel : TabDocumentViewModel
{
    public ResourceContentViewModel(IOpenDocumentManager openDocManager)
        : base(openDocManager)
    { }

    [ObservableProperty]
    private TextDocument? _text;
}
