using Muse.AudioProcessor.SoundTrackOperator;
using Muse.UI.Utilities;

namespace Muse.UI.ViewModel;

public class PlayBarVM : ViewModelBase
{
    #region Fields and Properties -------------------------------------------------------------------

    private string? _songTitle;

    public string? SongTitle
    {
        get => _songTitle;
        set
        {
            _songTitle = value;
            OnPropertyChanged();
        }
    }

    private string? _songPerformer;

    public string? SongPerformer
    {
        get => _songPerformer;
        set
        {
            _songPerformer = value;
            OnPropertyChanged();
        }
    }


    private TimeSpan _currentTimeStamp;

    public TimeSpan CurrentTimeStamp
    {
        get => _currentTimeStamp;
        set
        {
            _currentTimeStamp = value;
            OnPropertyChanged();
        }
    }


    private TimeSpan _songBasicDuration;

    public TimeSpan SongBasicDuration
    {
        get => _songBasicDuration;
        set
        {
            _songBasicDuration = value;
            OnPropertyChanged();
        }
    }

    #endregion of Fields and Properties -------------------------------------------------------------------

    public PlayBarVM()
    {
        // Subscribe to the CurrentTimeUpdated event, get the TimeSpan and raise the Event Handler
        AudioPlayer.CurrentTimeUpdated += OnCurrentTimeUpdated;
    }


    private void OnCurrentTimeUpdated(TimeSpan currentTime)
    {
        // Update CurrentTimeStamp with the received value
        CurrentTimeStamp = currentTime;
    }


    #region Basic Function: Play, Pause, CanUsePlayBar -------------------------------------------------------------------

    public RelayCommand PlayCommand => new(execute => Play(), canExecute => CanUsePlayBar());

    private void Play()
    {
        AudioPlayer.Play();
    }


    public RelayCommand PauseCommand => new(execute => Pause(), canExecute => CanUsePlayBar());

    private void Pause()
    {
        AudioPlayer.Pause();
    }

    private static bool CanUsePlayBar()
    {
        return AudioPlayer.AudioFile != null;
    }

    #endregion of basic function  -------------------------------------------------------------------


    #region Load a new song from outside, Update the header -------------------------------------------------------------------

    public void LoadAndPlaySong(string? songDetailLocalUrl, TimeSpan songBasicDuration)
    {
        AudioPlayer.Load(songDetailLocalUrl ?? throw new ArgumentNullException(nameof(songDetailLocalUrl)));
        SongBasicDuration = songBasicDuration;
        Play();
    }

    public void SetHeader(string songTitle, string songPerformer)
    {
        SongTitle = songTitle;
        SongPerformer = songPerformer;
    }

    #endregion -------------------------------------------------------------------
}