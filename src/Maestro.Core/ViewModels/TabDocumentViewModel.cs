using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Maestro.Core.Services.Contracts;

namespace Maestro.Core.ViewModels;

public abstract partial class TabDocumentViewModel : RecipientViewModelBase
{
    readonly IOpenDocumentManager _openDocManager;

    protected TabDocumentViewModel(IOpenDocumentManager openDocManager)
    {
        _openDocManager = openDocManager;
        this.Title = string.Empty;
    }

    [ObservableProperty]
    private string _title;

    [RelayCommand]
    private void Close()
    {
        _openDocManager.CloseDocument(this.Title);
    }
}
