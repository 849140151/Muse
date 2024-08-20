namespace Muse.UI.ViewModel;

public class MainWindowViewModel
{
    public SongListViewModel SongListViewModel { get; set; }

    public MainWindowViewModel(SongListViewModel songListViewModel)
    {
        SongListViewModel = songListViewModel;
    }
}