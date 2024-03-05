using Maestro.Core.Assets;
using Maestro.Core.Services.Contracts;

namespace Maestro.Core.ViewModels;

public class WelcomeViewModel : TabDocumentViewModel
{
    public WelcomeViewModel(IOpenDocumentManager openDocManager)
        : base(openDocManager)
    {
        this.Title = Resources.Welcome_TabTitle;
    }
}
