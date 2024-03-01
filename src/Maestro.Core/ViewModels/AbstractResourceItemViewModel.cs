using CommunityToolkit.Mvvm.ComponentModel;

namespace Maestro.Core.ViewModels;

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

    [ObservableProperty]
    private bool _isMouseOver;

    public abstract ResourceKind Kind { get; }
}
