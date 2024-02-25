using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace Maestro.Core.ViewModels;

public partial class SiteViewModel : ViewModelBase
{
    public SiteViewModel(string name)
    {
        this._siteName = name;
    }

    [ObservableProperty]
    private string _siteName;

    public ObservableCollection<AbstractResourceItemViewModel> Children { get; } = new();
}
