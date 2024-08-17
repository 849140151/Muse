using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using Muse.UI.MVVM;
using Muse.DB.Model;
using WinForms = System.Windows.Forms;

namespace Muse.UI.ViewModel;

public class SongListViewModel : ViewModelBase
{
    public ObservableCollection<SongBasic> Songs { get; set; } = [];

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
                        Songs.Add(new SongBasic()
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