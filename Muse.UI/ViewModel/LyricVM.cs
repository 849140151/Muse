using System.IO;
using System.Windows.Media.Imaging;
using Muse.UI.Utilities;
using TagLib;

namespace Muse.UI.ViewModel;

public class LyricVM : ViewModelBase
{
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
}