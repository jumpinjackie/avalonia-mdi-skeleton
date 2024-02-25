using Avalonia.Controls;
using Avalonia.Input;
using Maestro.Core.ViewModels;

namespace Maestro.Core.Views
{
    public partial class SiteView : UserControl
    {
        public SiteView()
        {
            InitializeComponent();
        }

        void OnTreeNodeDoubleTap(object? sender, TappedEventArgs e)
        {
            //TODO: The Grid where this DoubleTapped handler is registered for
            //clearly does not span the full width of the "node" area. We probably
            //have to bolt this handler to something higher-level, but what is that?
            if (e.Source is Control c && c.DataContext is ResourceItemViewModel rvm)
            {
                rvm.OpenCommand.Execute(null);
            }
        }
    }
}
