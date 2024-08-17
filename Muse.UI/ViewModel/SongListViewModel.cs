using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using Muse.DB.Configuration;
using Muse.UI.MVVM;
using Muse.DB.Model;
using WinForms = System.Windows.Forms;

namespace Muse.UI.ViewModel;

public class SongListViewModel : ViewModelBase
{
    public ObservableCollection<SongBasic> SongBasic { get; set; } = [];

    public SongListViewModel()
    {
        string slnFolder = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\.."));
        string dbPath = Path.Combine(slnFolder, "Muse.DB", "Muse.sqlite");
        Console.WriteLine(slnFolder);
        Console.WriteLine($"dbPath: {dbPath}");

        // using (MyDbContext db = new MyDbContext())
        // {
        //     var songBasics = db.SongBasic.Where(s => s.Performers == "tuki1").ToList();
        //     foreach (SongBasic songBasic in songBasics)
        //     {
        //         SongBasic.Add(songBasic);
        //     }
        // }
    }

    public RelayCommand SelectFolderCommand => new RelayCommand(execute => SelectFolder());
    private void SelectFolder()
    {
        // Get all the audio files in the selected folder
        using (var dialog = new FolderBrowserDialog())
        {
            if (dialog.ShowDialog() == WinForms.DialogResult.OK)
            {
                string[] audioExtensions = [".mp3", ".wav", ".flac", ".aac", ".m4a"];
                List<string> files = Directory.EnumerateFiles(dialog.SelectedPath)
                    .Where(file => audioExtensions.Contains(Path.GetExtension(file).ToLower()))
                    .ToList();
                if (files.Count != 0)
                {
                    foreach (string song in files)
                    {
                        SongBasic.Add(new SongBasic()
                        {
                            Title = Path.GetFileNameWithoutExtension(song)
                        });
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("No audio in this folder.", "Lack of audio", MessageBoxButton.OK);
                }
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