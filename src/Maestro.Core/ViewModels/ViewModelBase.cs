using CommunityToolkit.Mvvm.ComponentModel;

namespace Maestro.Core.ViewModels;

public class ViewModelBase : ObservableObject
{
    protected void ThrowIfNotDesignMode()
    {
        if (!Avalonia.Controls.Design.IsDesignMode)
            throw new System.Exception("This constructor was meant to be invoked only by the Avalonia designer, not by dependency injection or manually");
    }
}

public class RecipientViewModelBase : ObservableRecipient
{
    protected void ThrowIfNotDesignMode()
    {
        if (!Avalonia.Controls.Design.IsDesignMode)
            throw new System.Exception("This constructor was meant to be invoked only by the Avalonia designer, not by dependency injection or manually");
    }
}
