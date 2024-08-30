using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Muse.UI.ViewModel;
using UserControl = System.Windows.Controls.UserControl;

namespace Muse.UI.View;

public partial class Lyric : UserControl
{
    private readonly LyricVM _lyricVm;

    public Lyric()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    public Lyric(LyricVM _lyricVm)
    {
        this._lyricVm = _lyricVm;
        InitializeComponent();
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        // 确保 ViewModel 已经设置 DataContext
        if (DataContext is LyricVM viewModel)
        {
            viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }
    }

    private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(LyricVM.InMomentLyric))
        {
            Dispatcher.BeginInvoke(new Action(() => ScrollDown_Click(null, null)),
                System.Windows.Threading.DispatcherPriority.Background);
        }
    }


    #region Srcoll logic

    public void ScrollUp_Click(object sender, RoutedEventArgs e)
    {
        ScrollViewer scrollViewer = GetScrollViewer(LyricsListBox);
        if (scrollViewer != null)
        {
            scrollViewer.LineUp();
        }
    }


    public void ScrollDown_Click(object sender, RoutedEventArgs e)
    {
        ScrollViewer scrollViewer = GetScrollViewer(LyricsListBox);

        if (scrollViewer != null)
        {
            scrollViewer.LineDown();
        }
    }

    private ScrollViewer GetScrollViewer(DependencyObject depObj)
    {
        if (depObj is ScrollViewer)
        {
            return depObj as ScrollViewer;
        }
        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
        {
            var child = VisualTreeHelper.GetChild(depObj, i);
            var result = GetScrollViewer(child);
            if (result != null)
            {
                return result;
            }
        }
        return null;
    }
    #endregion


}