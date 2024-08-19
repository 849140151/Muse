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

    public SongListViewModel()
    {
        // Select all songs from database to initialize the songlist
        using (MyDbContext db = new MyDbContext())
        {
            var songBasics = db.SongBasic.ToList();
            SongBasic = new ObservableCollection<SongBasic>(songBasics);
        }
    }

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


    public RelayCommand PrintSongCommand => new RelayCommand(execute => PrintSong(), canExecute=> SelectSong != null);


    private void PrintSong()
    {
        Console.WriteLine(SelectSong.Title);
        SongDetail detail = new SongDetail();
    }

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



}