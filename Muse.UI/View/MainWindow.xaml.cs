﻿using System.Windows;
using Muse.UI.View;
using Muse.UI.ViewModel;

namespace Muse.UI;

public partial class MainWindow : Window
{
    public MainWindow(MainWindowViewModel mainWindowViewModel)
    {
        InitializeComponent();
        DataContext = mainWindowViewModel;

    }
}