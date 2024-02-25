﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Maestro.Core.Services.Contracts;
using Maestro.Core.Services.Messaging;
using Maestro.Core.Services.Stubs;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Maestro.Core.ViewModels;

public partial class MainViewModel : RecipientViewModelBase, IRecipient<OpenResourceMessage>, IRecipient<CloseResourceMessage>
{
    readonly IConnectionManager _connManager;
    readonly Func<ResourceContentViewModel> _createResourceContent;

    // Designer-only ctor
    public MainViewModel() 
    {
        _connManager = new StubConnectionManager();
        var orManager = new StubOpenResourceManager();
        _createResourceContent = () => new ResourceContentViewModel(orManager);
        _sidebar = new SidebarViewModel(
            name => new FolderItemViewModel(name),
            name => new ResourceItemViewModel(name, orManager));
        this.IsActive = true;
    }

    public MainViewModel(SidebarViewModel sidebar,
                         IConnectionManager connManager,
                         Func<ResourceContentViewModel> createResourceContent)
    {
        _sidebar = sidebar;
        _connManager = connManager;
        _createResourceContent = createResourceContent;
        this.IsActive = true;
    }

    [ObservableProperty]
    private SidebarViewModel _sidebar;

    public ObservableCollection<ResourceContentViewModel> OpenResources { get; } = new();

    /*
    [RelayCommand]
    private async Task ConnectToSite()
    {
        await _connManager.ConnectAsync();
    }
    */

    [ObservableProperty]
    private int _openResourceIndex;

    void IRecipient<OpenResourceMessage>.Receive(OpenResourceMessage message)
    {
        var ores = FindOpenResource(message.Name);
        if (ores != null)
        {
            this.OpenResourceIndex = this.OpenResources.IndexOf(ores);
        }
        else
        {
            var rvm = _createResourceContent();
            rvm.Title = message.Name;
            rvm.Text = message.Content;
            this.OpenResources.Add(rvm);
            this.OpenResourceIndex = this.OpenResources.Count - 1;
        }
    }

    private ResourceContentViewModel? FindOpenResource(string name) => this.OpenResources.FirstOrDefault(or => or.Title == name);

    void IRecipient<CloseResourceMessage>.Receive(CloseResourceMessage message)
    {
        var ores = FindOpenResource(message.Name);
        if (ores != null)
        {
            this.OpenResources.Remove(ores);
        }
    }
}
