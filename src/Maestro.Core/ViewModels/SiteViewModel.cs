using AvaloniaEdit.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Maestro.Core.Services.Contracts;
using Maestro.Core.Services.Messaging;
using Maestro.Core.Services.Stubs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Maestro.Core.ViewModels;

public partial class SiteViewModel : RecipientViewModelBase, IRecipient<FolderListedMessage>, IFolderItemViewModel
{
    readonly IViewModelFactory _vmFactory;

    // Designer-only ctor
    public SiteViewModel()
    {
        _vmFactory = new StubViewModelFactory();
        this.SiteName = "Test Site";
        this.Children.AddRange([
            _vmFactory.FolderItem().WithName("Test Site", "Samples", Enumerable.Empty<string>())
        ]);
        this.IsActive = true;
    }

    public SiteViewModel(IViewModelFactory vmFactory)
    {
        _vmFactory = vmFactory;
        this.IsActive = true;
    }

    public SiteViewModel WithName(string name)
    {
        this.SiteName = name;
        return this;
    }

    [ObservableProperty]
    private string? _siteName;

    public ObservableCollection<AbstractResourceItemViewModel> Children { get; } = new();

    private FolderItemViewModel? FindFolder(ArraySegment<string> path, IEnumerable<AbstractResourceItemViewModel> level)
    {
        foreach (var segment in path)
        {
            foreach (var folder in level.OfType<FolderItemViewModel>())
            {
                if (folder.Name == segment)
                {
                    if (path.Count == 1) //We're at the leaf level
                    {
                        return folder;
                    }
                    else
                    {
                        return FindFolder(path.Slice(1), folder.Children);
                    }
                }
            }
        }
        return null;
    }

    void IRecipient<FolderListedMessage>.Receive(FolderListedMessage message)
    {
        if (message.SiteName == this.SiteName)
        {
            var folder = FindFolder(message.List.ParentPath.ToArray(), Children);
            if (folder != null)
            {
                this.PopulateFolder(message.SiteName, message.List, folder);
            }
        }
    }

    internal void PopulateFolder(string siteName, ResourceListMessage list, IFolderItemViewModel folder)
    {
        foreach (var f in list.Folders)
        {
            folder.Children.Add(_vmFactory.FolderItem().WithName(siteName, f.Name, list.ParentPath));
        }
        foreach (var r in list.Resources)
        {
            folder.Children.Add(_vmFactory.ResourceItem().WithNameAndType(r.Name, r.Type));
        }
    }
}
