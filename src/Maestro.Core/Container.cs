using CommunityToolkit.Mvvm.Messaging;
using Maestro.Core.Services;
using Maestro.Core.Services.Contracts;
using Maestro.Core.Services.Stubs;
using Maestro.Core.ViewModels;
using StrongInject;

namespace Maestro;

[Register(typeof(MainViewModel), Scope.SingleInstance)]
[Register(typeof(SidebarViewModel), Scope.SingleInstance)]
[Register(typeof(ConnectViewModel), Scope.SingleInstance)]
[Register(typeof(FolderItemViewModel))]
[Register(typeof(ResourceItemViewModel))]
[Register(typeof(ResourceContentViewModel))]
[Register(typeof(WelcomeViewModel))]
[Register(typeof(MenuBuilder), Scope.SingleInstance)]
[Register(typeof(StubConnectionManager), Scope.SingleInstance, typeof(IConnectionManager))]
[Register(typeof(StubOpenDocumentManager), Scope.SingleInstance, typeof(IOpenDocumentManager))]
public partial class Container : IContainer<MainViewModel>
{
    [Factory(Scope.SingleInstance)]
    public IMessenger GetMessenger() => WeakReferenceMessenger.Default;
}