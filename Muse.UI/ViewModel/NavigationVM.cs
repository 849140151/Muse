﻿using System.Windows.Input;
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

    private readonly HomeVM _homeVm;
    public ICommand HomeCommand { get; set; }
    private void Home(object obj) => CurrentView = _homeVm;

    private readonly LyricVM _lyricVm;
    public ICommand LyricCommand { get; set; }
    private void Lyric(object obj) => CurrentView = _lyricVm;


    private readonly LyricManagerVM _lyricManagerVm;
    public ICommand LyricManagerCommand { get; set; }
    private void LyricManager(object obj) => CurrentView = _lyricManagerVm;

    /// <summary>
    ///     All the DataContext VM all inject here, and then pass to the MainWindow from App.xaml
    /// </summary>
    public NavigationVM(
        HomeVM homeVm, LyricVM lyricVm,
        PlayBarVM playBarVm, SongListVM songListVm,
        LyricManagerVM lyricManagerVm
        )
    {
        _songListVm = songListVm;
        SongListCommand = new RelayCommand(SongList);

        _homeVm = homeVm;
        HomeCommand = new RelayCommand(Home);

        _lyricManagerVm = lyricManagerVm;
        LyricCommand = new RelayCommand(Lyric);

        _lyricVm = lyricVm;
        LyricManagerCommand = new RelayCommand(LyricManager);


        CurrentView = _homeVm;
        PlayBarView = playBarVm;

    }

}