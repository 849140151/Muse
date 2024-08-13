using System.IO;
using System.Windows;
using WinForms = System.Windows.Forms;
namespace Muse.UI;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    public void btn_SelectFolder_Click(object sender, RoutedEventArgs e)
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
                        SongList.Items.Add(Path.GetFileNameWithoutExtension(song));
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("No audio in this folder.", "Lack of audio", MessageBoxButton.OK);
                }
            }
        }
    }



}