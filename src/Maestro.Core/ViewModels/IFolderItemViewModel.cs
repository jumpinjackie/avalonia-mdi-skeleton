using System.Collections.ObjectModel;

namespace Maestro.Core.ViewModels;

public interface IFolderItemViewModel
{
    ObservableCollection<AbstractResourceItemViewModel> Children { get; }
}
