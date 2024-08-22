using Muse.UI.ViewModel;
using UserControl = System.Windows.Controls.UserControl;

namespace Muse.UI.View;

public partial class Home : UserControl
{
    public Home()
    {
        InitializeComponent();
    }

    public Home(HomeVM homeVm)
    {
        InitializeComponent();
        DataContext = homeVm;
    }
}