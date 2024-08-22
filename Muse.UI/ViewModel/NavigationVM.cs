using System.Windows.Input;
using Muse.UI.Utilities;

namespace Muse.UI.ViewModel;

public class NavigationVM : ViewModelBase
{
    private object? _currentView;
    public object? CurrentView
    {
        get => _currentView;
        set
        {
            _currentView = value;
            OnPropertyChanged();
        }
    }

    private object? _playBarView;
    public object? PlayBarView
    {
        get => _playBarView;
        set
        {
            _playBarView = value;
            OnPropertyChanged();
        }
    }

    private readonly SongListVM _songListVm;

    public ICommand SongListCommand { get; set; }

    private void SongList(object obj) => CurrentView = _songListVm;

    public NavigationVM(SongListVM songListVM, PlayBarVM playBarVM)
    {
        _songListVm = songListVM;
        SongListCommand = new RelayCommand(SongList);

        CurrentView = _songListVm;
        PlayBarView = playBarVM;

    }

}