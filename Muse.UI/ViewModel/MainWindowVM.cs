namespace Muse.UI.ViewModel;

public class MainWindowVM
{
    private readonly NavigationVM NavigationVm;
    public PlayBarVM PlayBarVm { get; set; }
    public SongListVM SongListVm { get; set; }

    public MainWindowVM(SongListVM songListVm, PlayBarVM playBarVm, NavigationVM navigationVm)
    {
        NavigationVm = navigationVm;
        PlayBarVm = playBarVm;
        SongListVm = songListVm;
    }
}