using Muse.UI.ViewModel;
using UserControl = System.Windows.Controls.UserControl;

namespace Muse.UI.View;

public partial class Lyric : UserControl
{
    public Lyric()
    {
        InitializeComponent();
    }

    public Lyric(LyricVM lyricVm)
    {
        InitializeComponent();
        DataContext = lyricVm;
    }

}