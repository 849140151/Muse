using Muse.AudioProcessor.SoundTrackOperator;
using Muse.UI.Utilities;

namespace Muse.UI.ViewModel;

public class PlayBarVM : ViewModelBase
{
    #region Fields and Properties -------------------------------------------------------------------

    private bool _isUserDragging = false;

    private TimeSpan _currentTimeStamp;

    public TimeSpan CurrentTimeStamp
    {
        get => _currentTimeStamp;
        set
        {
            if (_currentTimeStamp != value)
            {
                _currentTimeStamp = value;
                OnPropertyChanged();
                // Make sure the change of CurrentTimeStamp can notify the slider bar
                OnPropertyChanged(nameof(CurrentTimeSeconds));
            }
        }
    }

    public double CurrentTimeSeconds
    {
        get => _currentTimeStamp.TotalSeconds;
        set
        {
            CurrentTimeStamp = TimeSpan.FromSeconds(value);
            if (_isUserDragging)
            {
                AudioPlayer.SetPosition(CurrentTimeStamp);
            }
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
        if (!_isUserDragging)
        {
            CurrentTimeStamp = currentTime;
        }
    }


    #region Basic Function: Play, Pause, CanUsePlayBar -------------------------------------------------------------------

    private bool _isChecked;
    public bool IsChecked
    {
        get => _isChecked;
        set
        {
            if (_isChecked == value) return;
            _isChecked = value;
            OnPropertyChanged();
        }
    }
    public RelayCommand PlayOrPauseCommand => new(execute => PlayOrPause(), canExecute => CanUsePlayBar());

    private void PlayOrPause()
    {
        if (IsChecked) AudioPlayer.Pause();
        else AudioPlayer.Play();
    }

    private static bool CanUsePlayBar()
    {
        return AudioPlayer.AudioFile != null;
    }

    #endregion of basic function  -------------------------------------------------------------------


    #region Load a new song from outside, Update the header -------------------------------------------------------------------

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

    public void LoadAndPlaySong(string? songDetailLocalUrl, TimeSpan songBasicDuration)
    {
        AudioPlayer.Load(songDetailLocalUrl ?? throw new ArgumentNullException(nameof(songDetailLocalUrl)));
        SongBasicDuration = songBasicDuration;
        IsChecked = false; // Reset the state, otherwise user have to double-click a song and hit the button to play the song
        AudioPlayer.SetVolume(_volume/100); // Reset the volume from the UI, divided by 100 is because the UI is from 0 to 100
        PlayOrPause();
    }

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
    public void SetHeader(string songTitle, string songPerformer)
    {
        SongTitle = songTitle;
        SongPerformer = songPerformer;
    }

    #endregion -------------------------------------------------------------------


    #region DragCommand for position slider bar -------------------------------------------------------------------


    // Control stream   xaml -> xaml.cs -> VM

    public RelayCommand DragStartedCommand => new(execute => OnDragStarted(), canExecute => CanUsePlayBar());

    private void OnDragStarted()
    {
        _isUserDragging = true;
    }

    public RelayCommand DragCompletedCommand => new(execute => OnDragCompleted(CurrentTimeSeconds), canExecute => CanUsePlayBar());

    private void OnDragCompleted(double newValue)
    {
        if (_isUserDragging)
        {
            AudioPlayer.SetPosition(TimeSpan.FromSeconds(newValue));
            _isUserDragging = false;
        }
    }

    #endregion

    #region Volumn Slider -------------------------------------------------------------------

    private float _volume = 20; // Initialize the volume
    public float Volume
    {
        get => _volume;
        set
        {
            _volume = value;
            AudioPlayer.SetVolume(value/100); // Divided by 100 is because the UI is from 0 to 100
            OnPropertyChanged();
        }
    }

    public RelayCommand MutingCommand => new(execute => Muting(), canExecute => CanUsePlayBar());

    private void Muting()
    {
        if (Volume != 0)
        {
            AudioPlayer.SetVolume(0f);
        }
        else
        {
            AudioPlayer.SetVolume(_volume/100);
        }
    }

    #endregion



}