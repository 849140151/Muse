using System.Collections.ObjectModel;
using System.Windows;
using Muse.AudioProcessor.SoundTrackOperator;
using Muse.AudioProcessor.Utils;
using Muse.DB.Configuration;
using Muse.DB.Model;
using Muse.UI.Utilities;
using MessageBox = System.Windows.MessageBox;

namespace Muse.UI.ViewModel;

public class SongListVM : ViewModelBase
{
    private readonly MyDbContext _dbContext;
    private readonly PlayBarVM _playBarVm;
    private readonly LyricVM _lyricVm;
    private readonly AudioManager _audioManager;

    public ObservableCollection<SongBasic> SongBasic { get; set; }

    private SongBasic? _selectSong;

    public SongBasic? SelectSong
    {
        get => _selectSong;
        set
        {
            _selectSong = value;
            OnPropertyChanged();
        }
    }

    public SongListVM(MyDbContext dbContext, PlayBarVM playBarVm, LyricVM lyricVm, AudioManager audioManager)
    {
        _dbContext = dbContext;
        _playBarVm = playBarVm;
        _lyricVm = lyricVm;
        _audioManager = audioManager;
        // Select all songs from database to initialize the song list
        var songBasics = _dbContext.SongBasic.ToList();
        SongBasic = new ObservableCollection<SongBasic>(songBasics);
    }

    #region Handle double click event

    public RelayCommand OnSongDoubleClickCommand => new(execute => OnSongDoubleClick());

    private void OnSongDoubleClick()
    {
        // Send the LocalUrl to PlayBarVm and play the song
        var selectSongDetail = _dbContext.SongDetail
            .First(s => s.SongBasicId == _selectSong!.SongBasicId);
        string localUrl = selectSongDetail.LocalUrl!;
        _playBarVm.LoadAndPlaySong(localUrl, _selectSong!.Duration);
        _playBarVm.SetHeader(_selectSong!.Title, _selectSong.Performers);

        // Send the Picture to LyricVM and show it
        _audioManager.Load(localUrl);
        if (_audioManager.Cover != null)
        {
            _lyricVm.SetSongCover(_audioManager.Cover);
            _playBarVm.SetSongCover(_audioManager.Cover);
        }

        // Get the lyrics for the song
        _lyricVm.GetLyrics(_selectSong.SongBasicId);
    }

    #endregion

    #region Select a folder and get the songs in it

    public RelayCommand SelectFolderCommand => new(execute => SelectFolder());

    private void SelectFolder()
    {
        // Get all the audio files in the selected folder
        using (var dialog = new FolderBrowserDialog())
        {
            if (dialog.ShowDialog() != DialogResult.OK) return;

            var audioFiles = FileUtils.FilterAudioFiles(dialog.SelectedPath);
            if (!audioFiles.Any())
            {
                MessageBox.Show("No audio in this folder.", "Lack of audio", MessageBoxButton.OK);
                return;
            }

            var newSongs = audioFiles
                .Select(audio =>
                {
                    _audioManager.Load(audio);
                    return _audioManager.SongBasic;
                })
                .Where(x => x != null)
                .Distinct(); // Remove duplicate songs, need to rewrite the Equals and GetHashCode methods of SongBasic

            foreach (var newSong in newSongs) SongBasic.Add(newSong ?? throw new InvalidOperationException());
        }
    }

    #endregion

    #region Save songs to database

    public RelayCommand SaveSongCommand => new(execute => SaveSong(), canExecute => CanSave());

    /// <summary>
    ///     Saving batch of the SongBasic to database
    /// </summary>
    /// <remarks>
    ///     Because all the new songs are dump into [ObservableCollection SongBasic] here <br />
    ///     So directly Distinct() this one, then select the one w/o SongBasicId <br />
    ///     So we can rule out the one which is the same with the old songs, <br />
    ///     Then take the one which is from the database <br />
    ///     And then save the left one to the database <br />
    ///     But still a problem: how can we know the song Distinct() at first is from outside or from database? <br />
    ///     If Distinct() is deleting the one from database, it will cause a repeated song save to database <br />
    /// </remarks>
    private void SaveSong()
    {
        var existingSongs = _dbContext.SongBasic.ToList();
        var existingSongIds = new HashSet<int>(existingSongs.Select(s => s.SongBasicId));

        var allSongs = SongBasic
            .Distinct()
            .Where(song => !existingSongIds.Contains(song.SongBasicId))
            .ToList();
        // Todo: from actual preforms seems like it can work well, but the problem state in remarks can still be happend

        _dbContext.SongBasic.AddRange(allSongs);
        _dbContext.SaveChanges();

        var updatedSongs = _dbContext.SongBasic.ToList();
        SongBasic.Clear(); // Clear the collection first
        foreach (var song in updatedSongs) SongBasic.Add(song);
    }

    private bool CanSave()
    {
        return true;
    }

    #endregion

    #region Delete songs to database

    public RelayCommand DeleteSongCommand => new(execute => DeleteSong(), canExecute => SelectSong != null);

    private void DeleteSong()
    {
        // saving the SongBasic to database

        // SongBasic.Remove(SelectSong);
        // Find that sqlite can not delete cascade, but EF core can.
        _dbContext.SongBasic.Remove(SelectSong!);
        _dbContext.SaveChanges();
        SongBasic.Remove(SelectSong!);
    }

    #endregion

    #region Demo function for using RelayCommand

    public RelayCommand PrintSongCommand => new(execute => PrintSong(), canExecute => SelectSong != null);

    private void PrintSong()
    {
        Console.WriteLine(SelectSong!.Title);
        var detail = new SongDetail();
    }

    #endregion
}