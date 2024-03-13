using AvaloniaEdit.Document;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Maestro.Core.Services;
using Maestro.Core.Services.Contracts;
using Maestro.Core.Services.Messaging;
using Maestro.Core.Services.Stubs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Maestro.Core.ViewModels;

public partial class MainViewModel : RecipientViewModelBase, IRecipient<OpenDocumentMessage>, IRecipient<CloseDocumentMessage>
{
    readonly IConnectionManager _connManager;
    readonly IViewModelFactory _vmFactory;

    // Designer-only ctor
    public MainViewModel() 
    {
        base.ThrowIfNotDesignMode();
        _connManager = new StubConnectionManager(WeakReferenceMessenger.Default);
        _vmFactory = new StubViewModelFactory();
        _sidebar = new SidebarViewModel(_vmFactory);
        this.IsActive = true;
        this.MenuItems = new MenuBuilder().Build(this);
    }

    public MainViewModel(SidebarViewModel sidebar,
                         IConnectionManager connManager,
                         IViewModelFactory vmFactory,
                         MenuBuilder menuBuilder)
    {
        _sidebar = sidebar;
        _connManager = connManager;
        _vmFactory = vmFactory;
        this.IsActive = true;
        this.MenuItems = menuBuilder.Build(this);
    }

    public IReadOnlyList<MenuItemViewModel> MenuItems { get; }

    [ObservableProperty]
    private SidebarViewModel _sidebar;

    public ObservableCollection<TabDocumentViewModel> OpenTabs { get; } = new();

    [ObservableProperty]
    private int _openTabIndex;

    protected override void OnActivated()
    {
        base.OnActivated();
        this.WelcomeCommand.Execute(null);
    }

    [RelayCommand]
    private void Options()
    {
        OpenOrActivateTab(t => t is OptionsViewModel, _vmFactory.Options);
    }

    [RelayCommand]
    private void Welcome()
    {
        OpenOrActivateTab(t => t is WelcomeViewModel, _vmFactory.Welcome);
    }

    private void OpenOrActivateTab<TViewModel>(Func<TabDocumentViewModel, bool> predicate,
                                               Func<TViewModel> factory)
        where TViewModel : TabDocumentViewModel
    {
        var wv = this.OpenTabs.FirstOrDefault(predicate);
        if (wv != null)
        {
            this.OpenTabIndex = this.OpenTabs.IndexOf(wv);
        }
        else
        {
            this.OpenTabs.Add(factory());
            this.OpenTabIndex = this.OpenTabs.Count - 1;
        }
    }

    void IRecipient<OpenDocumentMessage>.Receive(OpenDocumentMessage message)
    {
        OpenOrActivateTab(or => or.Title == message.Name, () =>
        {
            var rvm = _vmFactory.ResourceContent();
            rvm.Title = message.Name;
            rvm.Text = new TextDocument(message.Content?.ToString());
            return rvm;
        });
    }

    void IRecipient<CloseDocumentMessage>.Receive(CloseDocumentMessage message)
    {
        var ores = this.OpenTabs.FirstOrDefault(or => or.Title == message.Name);
        if (ores != null)
        {
            this.OpenTabs.Remove(ores);
        }
    }
}
