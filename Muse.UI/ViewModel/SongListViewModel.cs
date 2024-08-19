using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using Muse.AudioProcessor.SoundTrackOperator;
using Muse.DB.Configuration;
using Muse.UI.MVVM;
using Muse.DB.Model;
using WinForms = System.Windows.Forms;

namespace Muse.UI.ViewModel;

public class SongListViewModel : ViewModelBase
{
    public ObservableCollection<SongBasic> SongBasic { get; set; }

    public SongBasic selectSong;
    public SongBasic SelectSong
    {
        get => selectSong;
        set
        {
            selectSong = value;
            OnPropertyChanged();
        }
    }

    public SongListViewModel()
    {
        // Select all songs from database to initialize the songlist
        using (MyDbContext db = new MyDbContext())
        {
            var songBasics = db.SongBasic.ToList();
            SongBasic = new ObservableCollection<SongBasic>(songBasics);
        }
    }

    #region Selct a folder and get the songs in it
    public RelayCommand SelectFolderCommand => new RelayCommand(execute => SelectFolder());
    private void SelectFolder()
    {
        // Get all the audio files in the selected folder
        using (var dialog = new FolderBrowserDialog())
        {
            if (dialog.ShowDialog() != DialogResult.OK) return;

            List<string> audioFiles = AudioKnife.FilterAudioFiles(dialog.SelectedPath);
            if (!audioFiles.Any())
            {
                System.Windows.MessageBox.Show("No audio in this folder.", "Lack of audio", MessageBoxButton.OK);
                return;
            }

            IEnumerable<SongBasic?> newSongs = audioFiles
                .Select(x => AudioKnife.ReadAudioTags(x))
                .Where(x => x != null)
                .Distinct(); // Remove duplicate songs, need to rewrite the Equals and GetHashCode methods of SongBasic

            foreach (SongBasic? newSong in newSongs)
            {
                SongBasic.Add(newSong ?? throw new InvalidOperationException());
            }
        }
    }


    #endregion

    #region Save songs to database

    public RelayCommand SaveSongCommand => new RelayCommand(execute => SaveSong(), canExecute => CanSave());

    private void SaveSong()
    {
        // saving the SongBasic to database
        using (MyDbContext db = new MyDbContext())
        {
            // 获取数据库中已有的歌曲
            var existingSongs = db.SongBasic.ToList();
            var existingSongIds = new HashSet<int>(existingSongs.Select(s => s.SongBasicId));

            // 过滤掉已存在的歌曲
            var newSongs = SongBasic
                .Where(song => !existingSongIds.Contains(song.SongBasicId))
                .ToList();

            db.SongBasic.AddRange(newSongs);
            db.SaveChanges();
            // Todo: need to check the song if it exists in the database, if not, add it.
        }
    }

    private bool CanSave()
    {
        return true;
    }

    #endregion

    #region Delete songs to database

    public RelayCommand DeleteSongCommand => new RelayCommand(execute => DeleteSong(), canExecute => SelectSong != null);

    private void DeleteSong()
    {
        // saving the SongBasic to database
        using (MyDbContext db = new MyDbContext())
        {
            // SongBasic.Remove(SelectSong);
            // Find that sqlite can not delete cascade, but EF core can.
            var tukiSongs = db.SongBasic.First(s => s.Performers == "tuki.");
            db.Remove(tukiSongs);
            db.SaveChanges();

        }
    }

    #endregion


    #region Demo function for using RelayCommand

    public RelayCommand PrintSongCommand => new RelayCommand(execute => PrintSong(), canExecute=> SelectSong != null);
    private void PrintSong()
    {
        Console.WriteLine(SelectSong.Title);
        SongDetail detail = new SongDetail();
    }

    #endregion





}