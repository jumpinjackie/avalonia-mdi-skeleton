using System.Collections.Generic;
using System.Windows.Input;

namespace Maestro.Core.ViewModels;

public class MenuItemViewModel
{
    public required string Header { get; set; }
    
    public ICommand? Command { get; set; }
    
    public object? CommandParameter { get; set; }
    
    public IList<MenuItemViewModel>? Items { get; set; }
}
