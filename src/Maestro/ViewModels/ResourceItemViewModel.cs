using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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
    public ResourceItemViewModel(string name) : base(name)
    {
    }

    public override ResourceKind Kind => ResourceKind.Resource;
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