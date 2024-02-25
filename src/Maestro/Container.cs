using Maestro.Services;
using Maestro.ViewModels;
using StrongInject;

namespace Maestro;

[Register(typeof(MainViewModel), Scope.SingleInstance)]
[Register(typeof(SidebarViewModel), Scope.SingleInstance)]
[Register(typeof(AppServices), Scope.SingleInstance)]
public partial class Container : IContainer<MainViewModel>
{ }