using Maestro.Services;
using Maestro.ViewModels;
using StrongInject;

namespace Maestro;

[Register(typeof(MainViewModel))]
[Register(typeof(SidebarViewModel))]
[Register(typeof(AppServices), Scope.SingleInstance)]
public partial class Container : IContainer<MainViewModel>
{
    public static Container Instance { get; } = new();
}
