namespace Muse.UI.ViewModel;

public class MainWindowVM
{
    private NavigationVM NavigationVm { get; set; }
    public PlayBarVM PlayBarVm { get; set; }
    public SongListVM SongListVm { get; set; }
    public HomeVM HomeVm { get; set; }

    public MainWindowVM(
        SongListVM songListVm, PlayBarVM playBarVm,
        NavigationVM navigationVm, HomeVM homeVm
        )
    {
        NavigationVm = navigationVm;
        PlayBarVm = playBarVm;
        SongListVm = songListVm;
        HomeVm = homeVm;

    }
}