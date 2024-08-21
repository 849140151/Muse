using Muse.UI.MVVM;

namespace Muse.UI.ViewModel;

public class PlayBarViewModel : ViewModelBase
{

    #region Play Song

    public RelayCommand PlayCommand => new RelayCommand(execute => Play(), canExecute => CanUsePlayBar());

    private void Play()
    {
        AudioPlayer.Play();
    }

    #endregion

    #region Pause Song

    public RelayCommand PauseCommand => new RelayCommand(execute => Pause(), canExecute => CanUsePlayBar());

    private void Pause()
    {
        AudioPlayer.Pause();
    }

    #endregion

    #region Stop Song

    public RelayCommand StopCommand => new RelayCommand(execute => Stop(), canExecute => CanUsePlayBar());

    private void Stop()
    {
        AudioPlayer.Stop();
    }

    #endregion


    private bool CanUsePlayBar()
    {
        return true;
    }

    public void LoadAndPlaySong(string? songDetailLocalUrl)
    {
        AudioPlayer.Load(songDetailLocalUrl);
        AudioPlayer.Play();
    }
}