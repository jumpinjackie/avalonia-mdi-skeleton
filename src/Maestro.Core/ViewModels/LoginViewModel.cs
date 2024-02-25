using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Maestro.Core.Services.Contracts;
using Maestro.Core.Services.Stubs;
using System.Threading.Tasks;

namespace Maestro.Core.ViewModels;

public partial class LoginViewModel : ViewModelBase
{
    readonly IConnectionManager _connManager;

    // Designer-only ctor
    public LoginViewModel()
    {
        _site = "http://localhost:8008/mapguide/mapagent/mapagent.fcgi";
        _username = "Administrator";
        _password = "admin";
        _connManager = new StubConnectionManager();
    }

    public LoginViewModel(IConnectionManager connManager)
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

    [RelayCommand]
    private async Task Connect()
    {
        await _connManager.ConnectAsync(this.Site, this.Username, this.Password);
    }
}
