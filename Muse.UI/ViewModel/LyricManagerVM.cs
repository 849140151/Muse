using System.Collections.ObjectModel;
using Muse.DB.Configuration;
using Muse.DB.Model;
using Muse.UI.Utilities;

namespace Muse.UI.ViewModel;

public class LyricManagerVM : ViewModelBase
{
    private readonly MyDbContext _dbContext;
    public ObservableCollection<SongBasic> SongBasics { get; set; }
    public ObservableCollection<SongLyric> SongLyrics { get; set; }




    public LyricManagerVM(MyDbContext dbContext)
    {
        _dbContext = dbContext;
        SongBasics = new ObservableCollection<SongBasic>();
        SongLyrics = new ObservableCollection<SongLyric>();
    }

    #region SearchLyricCommand: Base the SongBasic.Title get and add the lyrcis to the SongLyrics


    public RelayCommand SearchLyricCommand => new(execute => SearchLyrics(""));

    private void SearchLyrics(string songBasicTitle)
    {
        _dbContext.SongBasic
            .Where(b => b.Title.Contains(songBasicTitle))
            .ToList();

    }


    #endregion


    #region Search song

    private string? _titleInput;
    public string? TitleInput
    {
        get => _titleInput;
        set
        {
            _titleInput = value;
            OnPropertyChanged();
        }
    }

    private string? _performerInput;
    public string? PerformerInput
    {
        get => _performerInput;
        set
        {
            _performerInput = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand SearchSongCommand => new(execute => SearchSong(TitleInput, PerformerInput));

    private void SearchSong(string? songBasicTitle, string? songBasicPerformer)
    {
        var songs = _dbContext.SongBasic
            .Where(b => b.Title.Contains(songBasicTitle!) || b.Performers.Contains(songBasicPerformer!))
            .ToList();
        SongBasics.Clear();
        foreach (var song in songs) SongBasics.Add(song);

    }




    #endregion

    #region SelectSong

    private SongBasic? _selectedSongBasic;
    public SongBasic? SelectedSongBasic
    {
        get => _selectedSongBasic;
        set
        {
            _selectedSongBasic = value;
            OnPropertyChanged();
            GetSongLyrics();
        }
    }

    private void GetSongLyrics()
    {
        List<SongLyric> songLyrics = _dbContext.SongLyric
            .Where(l => l.SongBasicId == _selectedSongBasic.SongBasicId)
            .OrderBy(l => l.LyricTimeStamp)
            .ToList();
        foreach (SongLyric songLyric in songLyrics) SongLyrics.Add(songLyric);
    }

    #endregion

    #region Select Lyric

    private SongLyric? _selectedSongLyric;
    public SongLyric? SelectedSongLyric
    {
        get => _selectedSongLyric;
        set
        {
            _selectedSongLyric = value;
            OnPropertyChanged();
        }
    }

    #endregion
}