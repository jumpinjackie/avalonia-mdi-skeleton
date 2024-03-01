using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Maestro.Core.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Maestro.Core.ViewModels;

public partial class FolderItemViewModel : AbstractResourceItemViewModel, IFolderItemViewModel
{
    readonly IConnectionManager _connManager;
    private string[] _path;
    private string _siteName;
    private bool _fetched;

    public FolderItemViewModel(IConnectionManager connManager)
    {
        _isExpanded = true;
        _fetched = false;
        _siteName = string.Empty;
        _path = Array.Empty<string>();
        _connManager = connManager;
    }

    public FolderItemViewModel WithName(string siteName, string name, IEnumerable<string> path)
    {
        this.Name = name;
        _siteName = siteName;
        _path = path.Concat([name]).ToArray();
        return this;
    }

    [ObservableProperty]
    private bool _isExpanded;

    /*
    async partial void OnIsExpandedChanged(bool value)
    {
        if (value)
        {
            if (!_fetched)
            {
                await _connManager.ListResourcesAsync(_siteName, _path);
                _fetched = true;
            }
        }
    }
    */

    [RelayCommand]
    private async Task Expand() 
    {
        if (!_fetched)
        {
            await _connManager.ListResourcesAsync(_siteName, _path);
            _fetched = true;
        }
        this.IsExpanded = true;
    }

    public ObservableCollection<AbstractResourceItemViewModel> Children { get; } = new();

    public override ResourceKind Kind => ResourceKind.Folder;
}