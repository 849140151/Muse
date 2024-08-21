namespace Muse.UI.ViewModel;

public class MainWindowViewModel
{
    public PlayBarViewModel PlayBarViewModel { get; set; }
    public SongListViewModel SongListViewModel { get; set; }

    public MainWindowViewModel(SongListViewModel songListViewModel, PlayBarViewModel playBarViewModel)
    {
        PlayBarViewModel = playBarViewModel;
        SongListViewModel = songListViewModel;
    }
}