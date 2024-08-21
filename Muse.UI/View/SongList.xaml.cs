using Muse.UI.ViewModel;
using UserControl = System.Windows.Controls.UserControl;

namespace Muse.UI.View;

public partial class SongList : UserControl
{
    public SongList()
    {
        InitializeComponent();
    }

    public SongList(SongListVM songListVm)
    {
        InitializeComponent();
        DataContext = songListVm;
    }
}