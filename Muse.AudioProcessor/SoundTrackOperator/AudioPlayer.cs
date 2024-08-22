using NAudio.Wave;
using T = System.Timers;

public static class AudioPlayer
{
    public static IWavePlayer waveOut;
    private static AudioFileReader audioFile;

    public static void Load(string filePath)
    {
        Dispose(); // 释放之前的资源
        waveOut = new WaveOutEvent();
        audioFile = new AudioFileReader(filePath);
        waveOut.Init(audioFile);
    }

    public static void Play()
    {
        waveOut.Play();
        // while (waveOut.PlaybackState == PlaybackState.Playing)
        // {
        //     Thread.Sleep(100);
        // }
    }

    public static void Pause()
    {
        waveOut.Pause();
    }

    public static void Stop()
    {
        waveOut.Stop();
        if (audioFile != null) audioFile.Position = 0;
    }

    public static void SetPosition(TimeSpan position)
    {
        if (audioFile != null && position < audioFile.TotalTime) audioFile.CurrentTime = position;
    }

    public static void SetVolume(float volume)
    {
        if (audioFile != null) audioFile.Volume = volume; // 0.0 to 1.0
    }

    public static void ShowCurrentTime()
    {
        Console.WriteLine($"Current Time: {audioFile.CurrentTime}");

    }

    public static void StartTimer()
    {
        T.Timer timer = new T.Timer(1000);
        timer.Elapsed += (sender, e) => ShowCurrentTime();
        timer.AutoReset = true;
        timer.Start();
    }




    public static void Dispose()
    {
        waveOut?.Dispose();
        audioFile?.Dispose();
        waveOut = null;
        audioFile = null;
    }
}