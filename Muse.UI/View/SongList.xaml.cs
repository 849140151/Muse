using System.ComponentModel;
using UserControl = System.Windows.Controls.UserControl;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using Muse.UI.MVVM;
using Muse.UI.ViewModel;
using WinForms = System.Windows.Forms;

namespace Muse.UI.View;
public partial class SongList : UserControl
{

    public SongList()
    {
        InitializeComponent();
        var vm = new SongListViewModel();
        DataContext = vm;
    }

}