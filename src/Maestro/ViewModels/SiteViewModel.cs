using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Maestro.ViewModels;

public partial class SiteViewModel : ViewModelBase
{
    private SidebarViewModel _parent;

    public SiteViewModel(SidebarViewModel parent, string name)
    {
        _parent = parent;
        this.siteName = name;
    }

    [ObservableProperty]
    private bool isLoading;

    internal async Task AddAsync()
    {
        try
        {
            this.IsLoading = true;

            // Simulating an enumerate resources
            //await Task.Delay(3000);
            var r = new Random();
            var numFolders = r.Next(3, 8);
            var numResources = r.Next(2, 4);
            foreach (int i in Enumerable.Range(0, numFolders))
            {
                this.Children.Add(new FolderItemViewModel("Folder " + i));
            }
            foreach (int i in Enumerable.Range(0, numResources))
            {
                this.Children.Add(new ResourceItemViewModel("Resource " + i));
            }
        }
        finally
        {
            this.IsLoading = false;
        }

        _parent.ConnectedSites.Add(this);
    }

    [ObservableProperty]
    private string siteName;

    public ObservableCollection<AbstractResourceItemViewModel> Children { get; } = new();
}
