using CommunityToolkit.Mvvm.Messaging;
using Maestro.Core.Assets;
using Maestro.Core.Services.Contracts;
using Maestro.Core.Services.Stubs;

namespace Maestro.Core.ViewModels;

public class OptionsViewModel : TabDocumentViewModel
{
    // Design-time ctor
    public OptionsViewModel()
        : base(new StubOpenDocumentManager(WeakReferenceMessenger.Default))
    {
        this.Title = Resources.Common_Options;
    }

    public OptionsViewModel(IOpenDocumentManager openDocManager)
        : base(openDocManager)
    {
        this.Title = Resources.Common_Options;
    }
}
