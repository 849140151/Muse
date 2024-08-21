using Muse.UI.ViewModel;
using UserControl = System.Windows.Controls.UserControl;

namespace Muse.UI.View;

public partial class PlayBar : UserControl
{
    public PlayBar()
    {
        InitializeComponent();
    }

    public PlayBar(PlayBarViewModel playBarViewModel)
    {
        InitializeComponent();
        DataContext = playBarViewModel;
    }
}