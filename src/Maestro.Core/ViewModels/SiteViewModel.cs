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
            new FolderItemViewModel().WithName("Samples")
        ]);
    }

    public SiteViewModel(IViewModelFactory vmFactory)
    {
        _vmFactory = vmFactory;
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
                
            }
        }
    }

    internal void PopulateFolder(ResourceListMessage root, IFolderItemViewModel folder)
    {
        foreach (var f in root.Folders)
        {
            folder.Children.Add(_vmFactory.FolderItem().WithName(f.Name));
        }
        foreach (var r in root.Resources)
        {
            folder.Children.Add(_vmFactory.ResourceItem().WithNameAndType(r.Name, r.Type));
        }
    }
}
