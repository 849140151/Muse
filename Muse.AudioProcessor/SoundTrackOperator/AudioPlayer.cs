using NAudio.Wave;
using T = System.Timers;


namespace Muse.AudioProcessor.SoundTrackOperator;

/// <summary>
///     Always use the Load method at first
/// </summary>
public static class AudioPlayer
{
    private static WaveOutEvent? _waveOut;
    public static AudioFileReader? AudioFile;
    private static T.Timer? _timer;
    private static bool _timerRunning;

    public static event Action<TimeSpan>? CurrentTimeUpdated;


    /// <summary>
    ///     Load audio file at first and initialize the waveOut and audioFile
    /// </summary>
    /// <param name="filePath"></param>
    public static void Load(string filePath)
    {
        Dispose(); // Release previous resources

        _waveOut = new WaveOutEvent();
        AudioFile = new AudioFileReader(filePath);

        _waveOut.Init(AudioFile);
    }

    public static void Play()
    {
        _waveOut!.Play();
        StartTimer();
    }

    public static void Pause()
    {
        _waveOut!.Pause();
        StopTimer();
    }


    private static TimeSpan GetCurrentTime()
    {
        return AudioFile!.CurrentTime;
    }

    private static void OnTimerElapsed()
    {
        // Raise the CurrentTimeUpdated event with the current time
        CurrentTimeUpdated?.Invoke(GetCurrentTime());
    }

    /// <summary>
    ///     A timer in AudioPlayer
    /// </summary>
    /// <remarks>
    ///     Update the CurrentTime by raised CurrentTimeUpdated <br/>
    ///     And all the component outside can subscribe this delegate to update their own time <br/>
    /// </remarks>
    /// <param name="interval"></param>
    private static void StartTimer(double interval = 1000)
    {
        if (_timerRunning) StopTimer();

        _timer = new T.Timer(interval);
        _timer.Elapsed += (sender, e) => OnTimerElapsed(); // Subscribe a method
        _timer.AutoReset = true;
        _timer.Start();
        _timerRunning = true;
    }


    private static void StopTimer()
    {
        if (!_timerRunning) return;
        _timer!.Stop();
        _timer.Dispose();
        _timer = null;
        _timerRunning = false;
    }

    public static void Dispose()
    {
        _waveOut?.Dispose();
        AudioFile?.Dispose();
        _waveOut = null;
        AudioFile = null;
    }

    public static void SetPosition(TimeSpan position)
    {
        if (position < AudioFile!.TotalTime) AudioFile.CurrentTime = position;
    }

    public static void SetVolume(float volume)
    {
        AudioFile!.Volume = volume; // 0.0 to 1.0
    }
}