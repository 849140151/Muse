namespace Muse.UI.ViewModel;

public class MainWindowVM
{
    public PlayBarVM PlayBarVm { get; set; }
    public SongListVM SongListVm { get; set; }

    public MainWindowVM(SongListVM songListVm, PlayBarVM playBarVm)
    {
        PlayBarVm = playBarVm;
        SongListVm = songListVm;
    }
}