using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Media.Imaging;
using Muse.AudioProcessor.LyricProcessor;
using Muse.AudioProcessor.SoundTrackOperator;
using Muse.DB.Configuration;
using Muse.DB.Model;
using Muse.UI.Utilities;
using TagLib;

namespace Muse.UI.ViewModel;

public class LyricVM : ViewModelBase
{
    private readonly MyDbContext _dbContext;
    private readonly PlayBarVM _playBarVm;

    public ObservableCollection<SongLyric> SongLyrics { get; set; }

    public LyricVM(MyDbContext dbContext, PlayBarVM playBarVm)
    {
        _dbContext = dbContext;
        _playBarVm = playBarVm;
        AudioPlayer.CurrentTimeUpdated += OnCurrentTimeUpdated;
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

    private void OnCurrentTimeUpdated(TimeSpan currentTime)
    {
        CurrentTimeStamp = currentTime;
        SongLyric? inMomentSongLyric = SongLyrics.FirstOrDefault(l => l.LyricTimeStamp == currentTime);


        if (inMomentSongLyric is null) return;
        InMomentLyric = inMomentSongLyric.Kanji;
        ParsedLyric = LyricTokenizer.ParseLyric(InMomentLyric!);
        // Only scroll the view when InMomentOrder > 5
        if (inMomentSongLyric.LyricOrder > 5) InMomentOrder = inMomentSongLyric.LyricOrder;
    }

    #region Get Lyrics Base on the SongListView Control

    public void GetLyrics(int songBasicId)
    {
        List<SongLyric> songLyrics = _dbContext.SongLyric
            .Where(l => l.SongBasicId == songBasicId)
            .ToList();
        SongLyrics = new ObservableCollection<SongLyric>(songLyrics);
    }

    #endregion

    #region Jump to the position when select a lyric

    private SongLyric? _selectSongLyric;
    public SongLyric? SelectSongLyric
    {
        get => _selectSongLyric;
        set
        {
            _selectSongLyric = value;
            OnPropertyChanged();
            OnSelectLyric(value!);
        }
    }

    private void OnSelectLyric(SongLyric songLyric)
    {
        _playBarVm.JumpToLyricTimeStamp(songLyric.LyricTimeStamp);
    }

    #endregion

    #region Showing Picture

    private BitmapImage? _songCover;

    public BitmapImage? SongCover
    {
        get => _songCover;
        set
        {
            _songCover = value;
            OnPropertyChanged();
        }
    }

    public void SetSongCover(IPicture iPicture)
    {
        SongCover = ConvertIPictureToBitmapImage(iPicture);
    }


    private BitmapImage ConvertIPictureToBitmapImage(IPicture iPicture)
    {
        using (var stream = new MemoryStream(iPicture.Data.Data))
        {
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = stream;
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            return bitmap;
        }
    }
    #endregion

    #region Show the lyric which singing right now

    private string? _inMomentLyric;

    public string? InMomentLyric
    {
        get => _inMomentLyric;
        set
        {
            _inMomentLyric = value;
            OnPropertyChanged();
        }
    }
    #endregion

    #region Parse the lyric

    private string? _parsedLyric;

    public string? ParsedLyric
    {
        get => _parsedLyric;
        set
        {
            _parsedLyric = value;
            OnPropertyChanged();
        }
    }

    #endregion

    #region Scorll the view

    private int? _inMomentOrder;

    public int? InMomentOrder
    {
        get => _inMomentOrder;
        set
        {
            _inMomentOrder = value;
            OnPropertyChanged();
        }
    }

    #endregion
}
