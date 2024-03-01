using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Maestro.Core.Services.Contracts;
using Maestro.Core.Services.Stubs;
using System;
using System.Threading.Tasks;

namespace Maestro.Core.ViewModels;

public partial class ConnectViewModel : ViewModelBase
{
    readonly IConnectionManager _connManager;

    // Designer-only ctor
    public ConnectViewModel()
    {
        _site = "http://localhost:8008/mapguide/mapagent/mapagent.fcgi";
        _username = "Administrator";
        _password = "admin";
        _connManager = new StubConnectionManager(WeakReferenceMessenger.Default);
    }

    public ConnectViewModel(IConnectionManager connManager)
    {
        _site = "http://localhost:8008/mapguide/mapagent/mapagent.fcgi";
        _username = "Administrator";
        _password = "admin";
        _connManager = connManager;
    }

    [ObservableProperty]
    private string _site;

    [ObservableProperty]
    private string _username;

    [ObservableProperty]
    private string _password;

    [ObservableProperty]
    private string? _connectError;

    [RelayCommand]
    private async Task Connect()
    {
        this.ConnectError = null;
        try
        {
            await _connManager.ConnectAsync(this.Site, this.Username, this.Password);
        }
        catch (Exception ex)
        {
            this.ConnectError = ex.Message;
        }
    }

    [RelayCommand]
    private void CancelConnect()
    {
        _connManager.CancelConnect();
    }
}
