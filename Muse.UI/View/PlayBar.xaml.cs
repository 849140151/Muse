﻿using System.Windows.Controls.Primitives;
using Muse.UI.ViewModel;
using UserControl = System.Windows.Controls.UserControl;

namespace Muse.UI.View;

public partial class PlayBar : UserControl
{
    public PlayBar()
    {
        InitializeComponent();
    }

    public PlayBar(PlayBarVM playBarVm)
    {
        InitializeComponent();
        DataContext = playBarVm;
    }

    private void Thumb_DragStarted(object sender, DragStartedEventArgs e)
    {
        var viewModel = DataContext as PlayBarVM;
        viewModel?.DragStartedCommand.Execute(null);
    }

    private void Thumb_DragCompleted(object sender, DragCompletedEventArgs e)
    {
        var viewModel = DataContext as PlayBarVM;
        viewModel?.DragCompletedCommand.Execute(PositionSlider.Value);
    }
}