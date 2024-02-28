using Maestro.Core.Services.Contracts;

namespace Maestro.Core.ViewModels;

public class OptionsViewModel : TabDocumentViewModel
{
    public OptionsViewModel(IOpenDocumentManager openDocManager)
        : base(openDocManager)
    {
        this.Title = "Options";
    }
}
