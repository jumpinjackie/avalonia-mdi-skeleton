using CommunityToolkit.Mvvm.ComponentModel;
using Maestro.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Maestro.ViewModels;

public partial class SiteViewModel : ViewModelBase
{
    public SiteViewModel(string name)
    {
        this.siteName = name;
    }

    [ObservableProperty]
    private string siteName;

    public ObservableCollection<AbstractResourceItemViewModel> Children { get; } = new();
}
