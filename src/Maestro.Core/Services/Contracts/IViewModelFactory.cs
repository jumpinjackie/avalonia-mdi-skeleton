using Maestro.Core.ViewModels;

namespace Maestro.Core.Services.Contracts;

public interface IViewModelFactory
{
    FolderItemViewModel FolderItem();
    
    OptionsViewModel Options();
    
    ResourceContentViewModel ResourceContent();
    
    ResourceItemViewModel ResourceItem();
    
    WelcomeViewModel Welcome();

    ConnectViewModel Connect();

    SiteViewModel Site();
}
