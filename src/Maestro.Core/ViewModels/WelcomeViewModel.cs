using Maestro.Core.Services.Contracts;

namespace Maestro.Core.ViewModels;

public class WelcomeViewModel : TabDocumentViewModel
{
    public WelcomeViewModel(IOpenDocumentManager openDocManager)
        : base(openDocManager)
    {
        this.Title = "Welcome";
    }
}
